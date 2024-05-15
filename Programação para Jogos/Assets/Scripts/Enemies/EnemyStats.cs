using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData; // Dados do ScriptableObject do inimigo

    // Estatísticas atuais
    [HideInInspector]
    public float currentMoveSpeed; // Velocidade de movimento atual
    [HideInInspector]
    public float currentHealth; // Vida atual
    [HideInInspector]
    public float currentDamage; // Dano atual
    public float despawnDistance = 20f; // Distância para ativar teleporte
    Transform player; // Dados do componente Transform do jogador

    //Informações de tomada de dano do inimigo
    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1); // Cor do dano (R, G, B, A) -> Vermelho
    public float damageFlashDuration = 0.2f; // Duração da animação de dano
    public float deathFadeTime = 0.6f; // Tempo de desaparecimento
    Color originalColor; // Cor original
    SpriteRenderer sr; // Dados do componente SpriteRenderer do inimigo
    EnemyMovement movement; // Movimento do inimigo

    void Awake() // Chamado antes do start
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color; // Define a cor original baseado na cor do sprite ao iniciar

        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) >= despawnDistance) // Se o inimigo estiver mais longe do que a distância para ativar o teleporte
        {
            ReturnEnemy(); // Retorna o inimigo para uma das posições relativas possíveis
        }
    }

    public void TakeDamage(float damage, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        currentHealth -= damage; // Diminui a vida pelo dano tomado
        StartCoroutine(DamageFlash()); // Inicia a rotina da animação de tomada de dano

        if(damage > 0) // Se o dano tomado for maior do que 0, gera o texto de dano
        {
            GameManager.GenerateFloatingText(Mathf.FloorToInt(damage).ToString(), transform);
        }
        
        if(knockbackForce > 0) // Se a força de recuo for maior do que 1, realiza o movimento de recuo
        {
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            movement.Knockback(dir.normalized * knockbackForce, knockbackDuration);
        }

        if(currentHealth <= 0) // Se a vida chegar a 0
        {
            Kill();
        }
    }

    IEnumerator DamageFlash()
    {
        sr.color = damageColor; // Vermelho
        yield return new WaitForSeconds(damageFlashDuration); // Pausa e retorna no próximo frame
        sr.color = originalColor; // Cor do sprite
    }

    IEnumerator KillFade() // Corotina para animação do inimigo desaparecendo
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame(); // Espera até o fim do frame
        float timeRunning = 0, origAlpha = sr.color.a; // timeRunning -> tempo em que está rodando a rotina, origAlpha -> Alpha do inimigo (R, G, B, A)

        while(timeRunning < deathFadeTime) // Enquanto o tempo em que está rodando for menor do que o tempo de desaparecimento
        {
            yield return wait; // Pausa e retorna no próximo frame
            timeRunning += Time.deltaTime; // Acrescenta ao tempo em que está rodando

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - timeRunning / deathFadeTime) * origAlpha); // Altera o Alpha de modo a ir diminuindo dentro do tempo estabelecido
        }

        Destroy(gameObject); // Destrói o inimigo
    }
    public void Kill() // Ao morrer, inicializa a corotina
    {
        StartCoroutine(KillFade());
    }

    private void OnCollisionStay2D(Collision2D col) // Causa dano utilizando o método TakeDamage() quanto colidindo com o jogador
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy() // Método da Unity ao destruir um objeto
    {
        if(!gameObject.scene.isLoaded)
        {
            return;
        }
        
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy() // Retorna o inimigo para um dos pontos relativos perto do jogador
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}

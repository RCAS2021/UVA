using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData; // Dados do ScriptableObject da arma

    protected Vector3 direction; // Direção
    public float destroyAfterSeconds; // Tempo até destruir
    
    // Current Stats
    protected float currentDamage; // Dano atual
    protected float currentSpeed; // Velocidade atual
    protected float currentCooldownDuration; // Tempo de recarga atual
    protected int currentPierce; // Penetração atual

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight; // Atualiza o dano pela força
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
        currentDamage = GetCurrentDamage();
    }
    public void DirectionChecker(Vector3 dir) // Checa a direção e transforma (transform) a arma
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirx < 0 && diry == 0) // Esquerda
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }

        else if(dirx == 0 && diry < 0) // Baixo
        {
            scale.y = scale.y * -1;
        }

        else if(dirx == 0 && diry > 0) // Cima
        {
            scale.x = scale.x * -1;
        }

        else if(dirx > 0 && diry > 0) // Direita Cima
        {
            rotation.z = 0f;
        }

        else if(dirx > 0 && diry < 0) // Direita baixo
        {
            rotation.z = -90f;
        }

        else if(dirx < 0 && diry > 0) // Esquerda Cima
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }

        else if(dirx < 0 && diry < 0) // Esquerda Baixo
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) // Ao colidir, chama os métodos de tomada de dano do inimigo ou do prop e reduz a penetração em 1
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, transform.position);
            ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ReducePierce();
            }
        }
    }

    void ReducePierce() // Reduz a penetração em 1
    {
        currentPierce --;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}

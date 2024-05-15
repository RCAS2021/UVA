using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;  // Dados do ScriptableObject do personagem

    // Estatísticas atuais
    float currentHealth; // Vida atual
    float currentRecovery; // Regeneração de vida atual
    float currentMoveSpeed; // Velocidade de movimento atual
    float currentMight; // Força atual
    float currentProjectileSpeed; // Velocidade de projétil atual
    float currentMagnet; // Imã atual

    // Region possibilita criar regiões do código, melhorando a visibilidade
    #region Current Stats Properties
    // Utilizando properties possibilita adicionar logica as estatísticas de forma mais simples que utilizando métodos
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            // Checa se o valor mudou
            if(currentHealth != value)
            {
                currentHealth = value; // Define o valor em tempo real
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Vida: " + currentHealth;
                } 
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            // Checa se o valor mudou
            if(currentRecovery != value)
            {
                currentRecovery = value; // Define o valor em tempo real
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Regeneração: " + currentRecovery;
                } 
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            // Checa se o valor mudou
            if(currentMoveSpeed != value)
            {
                currentMoveSpeed = value; // Define o valor em tempo real
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Velocidade de movimento: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            // Checa se o valor mudou
            if(currentMight != value)
            {
                currentMight = value; // Define o valor em tempo real
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Força: " + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            // Checa se o valor mudou
            if(currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value; // Define o valor em tempo real
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidade do projétil: " + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            // Checa se o valor mudou
            if(currentMagnet != value)
            {
                currentMagnet = value; // Define o valor em tempo real
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Imã: " + currentMagnet;
                }
            }
        }
    }
    #endregion

    // Experiência e nível
    [Header("Experience/Level")]
    public int experience = 0; // Experiência inicial
    public int level = 1; // Nível inicial
    public int experienceCap; // Limite de experiência

    // Possibilta ao campo ser visível e editável
    [System.Serializable]
    public class LevelRange // Classe para definir a faixa de nível e o limite de experiência para a faixa
    {
        public int startLevel; // Nível inicial
        public int endLevel; // Nível final
        public int experienceCapIncrease; // Aumento do limite de experiência
    }

    // Invincibility-Frames (Dados em que o personagem fica invencível)
    [Header("Invincibility-Frames")]
    public float invincibilityDuration; // Duração da invencibilidade
    float invincibilityTimer; // Temporizador da invencibilidade
    bool isInvincible; // Booleando para chegar se está invencível

    public List<LevelRange> levelRanges; // Lisa das faixas de nível

    InventoryManager inventory; // Dados do inventário
    public int weaponIndex; // Índice da arma
    public int passiveItemIndex; // Índice do item passivo

    [Header("UI")]
    public Image healthBar; // Imagem da barra de vida
    public Image expBar; // Imagem da barra de experiência
    public TMP_Text levelText; // TextMeshPro do texto do nível

    //Informações de tomada de dano do jogador
    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1); // Cor do dano (R, G, B, A) -> Vermelho
    public float damageFlashDuration = 0.2f; // Duração da animação de dano
    Color originalColor; // Cor original
    SpriteRenderer sr; // Dados do componente SpriteRenderer do jogador
    
    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        SpawnWeapon(characterData.StartingWeapon);
    }

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        // Coloca os respectivos dados nos campos de texto
        GameManager.instance.currentHealthDisplay.text = "Vida: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Regeneração: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Velocidade de movimento: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Força: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidade de projétil: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Imã: " + currentMagnet;

        // Atribui os dados do personagem ao personagem escolhido
        GameManager.instance.AssignChosenCharacterUI(characterData);
        sr = GetComponent<SpriteRenderer>(); // Sprite Renderer do personagem
        originalColor = sr.color; // Define a cor original baseado na cor do sprite ao iniciar

        // Atualiza os respectivos valores para os valores iniciais
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }

    private void Update()
    {
        if(invincibilityTimer > 0) // Decresce o timer de invencibilidade
        {
            invincibilityTimer -= Time.deltaTime;
        }

        else if(isInvincible)
        {
            isInvincible = false;
        }

        Recover(); // Regenera a vida
    }

    public void IncreaseExperience(int amount) // Acrescenta a experiência pela quantidade
    {
        experience += amount;

        LevelUpChecker(); // Checa se atingiu um novo nível
        UpdateExpBar(); // Atualiza a barra de experiência
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap) // Se a experiência é maior ou igual ao limite de experiência
        {
            currentHealth = characterData.MaxHealth; // Regenera a vida do personagem para o máximo
            level++;                                // Adiciona um nível
            experience -= experienceCap;           // Subtrai o limite de experiência da experiência (Se o limite for 100, estiver com 90 e receber 20, sobe um nível e mantém 10 de experiência)

            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges) // Aumenta o limite de experiência de acordo com as faixas de nível
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

        experienceCap += experienceCapIncrease;

        UpdateLevelText();  // Atualiza o texto de nível
        GameManager.instance.StartLevelUp(); // Chama o métood que inicializa o  estado de subida de nível
        }
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap; // Animação de preenchimento da barra de experiência
    }

    void UpdateLevelText()
    {
        levelText.text = "Level " +level.ToString();
    }

    public void TakeDamage(float damage)
    {
        if(!isInvincible) // Checa se o jogador não está invencível
        {
            CurrentHealth -= damage; // Diminui a vida pelo dano tomado
            StartCoroutine(DamageFlash()); // Inicia a rotina da animação de tomada de dano

            invincibilityTimer = invincibilityDuration; // Reseta o timer de invisibilidade para a duração
            isInvincible = true;

            if(CurrentHealth <= 0) // Se a vida chegar a 0 ou menos, chama o método Kill()
            {
                Kill();
            }

            UpdateHealthBar(); // Atualiza a barra de vida
        } 
    }

    IEnumerator DamageFlash()
    {
        sr.color = damageColor; // Vermelho
        yield return new WaitForSeconds(damageFlashDuration); // Pausa e retorna no próximo frame
        sr.color = originalColor; // Cor do sprite
    }

    void UpdateHealthBar(){
        healthBar.fillAmount = currentHealth / characterData.MaxHealth; // Animação de preenchimento da barra de vida
    }

    public void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }

    public void RestoreHealth(float amount) // Restaura vida
    {
        if(CurrentHealth < characterData.MaxHealth) // Se a vida atual for menor do que a vida máxima
        {
           if(amount > (characterData.MaxHealth - CurrentHealth)) // Se a quantidade for maior do que a vida máxima - vida atual, define a vida atual como a vida máxima. Senão, define a vida atual para a vida atual + quantidade
           {
            CurrentHealth = characterData.MaxHealth; // Define a vida atual como a vida máxima
           }
           else
           {
            CurrentHealth += amount; // Define a vida atual para a vida atual + quantidade
           }
        }

        UpdateHealthBar(); // Atualiza a barra de vida
    }

    void Recover() // Regeneração de vida
    {
        if(CurrentHealth < characterData.MaxHealth)  // Se a vida atual for menor do que a vida máxima
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime; // Acrescenta à vida atual o valor da regeneração de vida por tempo

            if(CurrentHealth > characterData.MaxHealth) // Se a vida atual for maior do que a vida máxima, define a vida atual como a vida máxima
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon) // Gera a arma
    {
        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            return;
        }
        // Gera a arma inicial
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); // Define a arma para ser filha do jogador
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); // Adiciona a arma ao espaço do inventário

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem) // Gera o item passivo
    {
        if(passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            return;
        }

        //Spawn the passive item
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform); // Define o item passive para ser filho do jogador
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>()); // Adiciona o item passivo ao espaço do inventário
        passiveItemIndex++;
    }
}

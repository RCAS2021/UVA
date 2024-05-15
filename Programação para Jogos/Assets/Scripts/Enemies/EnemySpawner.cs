using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave // Ondas de inimigos
    {
        public string waveName; // Nome da onda
        public List<EnemyGroup> enemyGroups; // Lista de grupos de inimigos que serão gerados nessa onda
        public int waveQuota; // Número total de inimigos que serão gerados nessa onda
        public float spawnInterval; // Intervalo de geração de inimigos
        public int spawnCount; // Quantidade de inimigos gerados atualmente
    }

    [System.Serializable]
    public class EnemyGroup // Grupos de inimigos
    {
        public string enemyName; // Nome do inimigo
        public int enemyCount; // Número de inimigos que serão gerados nessa onda
        public int spawnCount; // Quantidade de inimigos gerados atualmente
        public GameObject enemyPrefab; // Prefabs dos inimigos
    }

    public List<Wave> waves; // Lista de ondas
    public int currentWaveCount; // Onda atual

    [Header("Spawner Attributtes")]
    public int enemiesAlive; // Inimigos vivos
    public int maxEnemiesAllowed; // Número máximo de inimigos permitidos no mapa de uma vez
    public bool maxEnemiesReached = false; // Se o número máximo de inimigos foi alcançado
    bool isWaveActive = false; // Se a onda está ativa
    public float waveInterval; // Intervalo entre ondas
    float spawnTimer; // Temporizador
    
    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // Posições possíveis para gerar um inimigo

    Transform player; // Informações do Transform do jogador 

    
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota(); // Calcula a quota de inimigos da onda
    }

    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive) // Checa se a onda atual é menor do que o total de ondas, se o número de inimigos gerados é 0 e se isWaveActive é falso
        {
            StartCoroutine(BeginNextWave()); // Inicializa a próxima onda
        }

        spawnTimer += Time.deltaTime; // Acrescenta o tempo

        if(spawnTimer >= waves[currentWaveCount].spawnInterval) // Checa se o timer é maior do que o intervalo entre ondas
        {
            spawnTimer = 0f; // Reseta o timer
            SpawnEnemies(); // Gera os inimigos
        }
    }

    IEnumerator BeginNextWave() // Inicializa a próxima onda
    {
        isWaveActive = true;

        // Espera pelo intervalo entre ondas antes de começar a próxima onda
        yield return new WaitForSeconds(waveInterval);

        if(currentWaveCount < waves.Count - 1) // Se há mais ondas a serem iniciadas, move para a próxima onda
        {
            isWaveActive = false;
            currentWaveCount++; // Incrementa ao número da onda
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota() // Calcula se a quota foi atingida
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups) // Para cada grupo de inimigos na onda, adiciona a quantidade de inimigos gerados a uma variável
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota; // Adiciona à onda atual qual a quantidade atual de inimigos gerados

        Debug.Log(currentWaveQuota);
    }

    void SpawnEnemies() // Gera os inimigos
    {
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached) // Checa se o número mínimo de inimigos na onda foi gerado
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups) // Gera cada tipo de inimigo até que a quota seja atingida
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount) // Checa se o número mínimo de inimigos desse tipo foi gerado
                {   
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity); // Gera o inimigo em uma posição aleatória dentro das possíveis opções (relativeSpawnPoints) perto do personagem

                    enemyGroup.spawnCount++; // Acrescenta a quantidade de grupos de inimigos gerados
                    waves[currentWaveCount].spawnCount++; // Acrescenta a quantidade de ondas geradas
                    enemiesAlive++; // Acrescenta a variável com a quantidade de inimigos vivos 

                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
    }

    public void OnEnemyKilled() // Ao eliminar um inimigo
    {
        enemiesAlive--; // Reduz a variável com a quantidade de inimigos vivos
        
        if(enemiesAlive < maxEnemiesAllowed) // Verifica se a quantidade de inimigos vivos é menor do que a quantidade máxima de inimigos no mapa
        {
            maxEnemiesReached = false;
        }
    }
}

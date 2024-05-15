using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks; // Armazena os Prefabs dos diferentes chunks de terreno
    public GameObject player; // Referência à posição do jogador
    public float checkerRadius; // Raio de checagem
    public LayerMask terrainMask; // Acompanha qual Layer é terreno e qual não é
    public GameObject currentChunk; // Chunk atual
    Vector3 playerLastPosition; // Última posição do jogador

    [Header("Optimization")]
    public List<GameObject> spawnedChunks; // Lista com chunks gerados
    GameObject latestChunk; // Último chunk
    public float maxOpDist; // Distância máxima para otimização -> maior do que a largura e comprimento do tileMap
    float opDist; // Distância atual
    float optimizerCooldown; // Tempo de recarga da otimização atual
    public float optimizerCooldownDuration; // Tempo de recarga da otimização

    // Start is called before the first frame update
    void Start()
    {
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker() // Checa se outro chunk deve ser gerado
    {
        if(!currentChunk)
        {
            return;
        }

        Vector3 moveDir = player.transform.position - playerLastPosition; // Direção em que o jogador está movendo
        playerLastPosition = player.transform.position;                  // Posição do jogador

        string directionName = GetDirectionName(moveDir);               // Nome da direção

        string[] directions = new string[]{"Up", "Down", "Right", "Left"}; // Possíveis direções
     
        CheckAndSpawnChunk(directionName);
        
        foreach(string dir in directions)
        {
            if(directionName.Contains(dir))
            {
                CheckAndSpawnChunk(dir);
            }
        }
    }

    void CheckAndSpawnChunk(string direction) // Checa se já possui um chunk na direção em que será gerado e então gera o chunk
    {
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.y > 0.5f) // Movendo na horizontal
            {
                return direction.x > 0 ? "Right Up": "Left Up"; // Movendo Diagonal Cima
            }
            else if(direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down": "Left Down"; // Movendo diagonal baixo
            }
            else
            {
                return direction.x > 0 ? "Right": "Left"; // Movendo Horizontalmente
            }
        }
        else
        {
            if(direction.x > 0.5f) // Movendo na vertical
            {
                return direction.y > 0 ? "Right Up": "Right Down"; // Movendo diagonal direita
            }
            else if(direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up": "Left Down"; // Movendo diagonal esquerda
            }
            else
            {
                return direction.y > 0 ? "Up": "Down"; // Movendo verticalmente
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition) // Escolhe um chunk aleatório e o gera
    {
        int rand = Random.Range(0, terrainChunks.Count); // Número aleatório
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity); // Instancia o chunk recebendo o número aleatório
        spawnedChunks.Add(latestChunk); // Adiicona o chunk gerado à lista de chunks gerados
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime; // Checa cada tempo de otimização ao invés de frame

        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks) // Desabilita os chunks fora da distância máxima
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position); // Distância do jogador x chunk
            if(opDist > maxOpDist) // Se a distância for maior do que a distância máxima, desabilita o chunk
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player; // Dados do jogador
    CircleCollider2D playerCollector; // Círculo de colisão do coletor do jogador
    public float pullSpeed; // Velocidade de tração

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet; // Atualiza o raio do colisor pelo imã
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Checa se outro objeto de jogo tem a interface ICollectible, se tiver, chama o método Collect()
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movimento
    [HideInInspector]  // Esconde a variável no inspetor
    public float lastHorizontalVector; // Guarda o último X do personagem
    [HideInInspector]
    public float lastVerticalVector; // Guarda o último y do personagem
    [HideInInspector]
    public Vector2 moveDir; // Direção de movimento
    [HideInInspector]
    public Vector2 lastMovedVector; // Última direção movida

    // Referências
    Rigidbody2D rb; // Componente RidigBody2D do jogador
    PlayerStats player; // Dados do jogador


    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); // Inicializa o último vetor movido como indo para a direita     
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate() // Diferentemente do Update, é independente da Taxa de Quadros (Frame Rate)
    {
        Move();
    }

    void InputManagement()
    {
        if(GameManager.instance.isGameOver) // Se estiver no estado de fim de jogo, impede que sejam realizados movimentos
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal"); // Define o X
        float moveY = Input.GetAxisRaw("Vertical");  // Define o y

        moveDir = new Vector2(moveX, moveY).normalized;
        if(moveDir.x != 0) // Checa se o valor de x é igual a zero, senão, o jogador está movendo, atualiza o vetor com o último valor de x
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if(moveDir.y != 0) // Checa se o valor de y é igual a zero, senão, o jogador está movendo atualiza o vetor com o último valor de y
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if(moveDir.y != 0 && moveDir.x != 0) // Checa se os valores de x e y são igual a zero, senão, o jogador está movendo atualiza o vetor com o último valor de x e y
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        if(GameManager.instance.isGameOver) // Se estiver no estado de fim de jogo, impede que sejam realizados movimentos
        {
            return;
        }

        rb.velocity = new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed); // Move o jogador 
    }
}
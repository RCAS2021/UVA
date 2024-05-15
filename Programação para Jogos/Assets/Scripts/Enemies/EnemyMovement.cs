using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player; // Dados do componente Transform
    EnemyStats enemy; // Dados do inimigo
    public float lastHorizontalVector; // Último X
    Vector2 knockbackVelocity; // Velocidade do recuo
    float knockbackDuration; // Duração do recuo
    SpriteRenderer sr; // Dados do component SpriteRenderer

    public EnemyScriptableObject enemyData; // Dados do ScriptableObject do inimigo

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(knockbackDuration > 0) // Realiza o recuo ao tomar dano
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else // Movimento do inimigo, segue o jogador, altera o eixo X, dependendo do Sprite, o eixo deve ser alterado diferente dos outros (Drone e Robot)
        {
            Vector2 oldPos = transform.position; // Posição anterior
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); // Segue o jogador

            Vector2 movement = (Vector2)transform.position - oldPos; // Pega o vetor de movimento baseado na diferença do vetor atual com o antigo (Método para pegar a direção em que o inimigo está movendo)
            if(movement.x < 0 && enemyData.Name != "Drone" && enemyData.Name != "Robot") // Movendo para a esquerda
            {
                sr.flipX = true;
            }
            else if(movement.x > 0 && enemyData.Name != "Drone" && enemyData.Name != "Robot") // Movendo para a direita
            {
                sr.flipX = false;
            }
            else if(movement.x < 0 && (enemyData.Name == "Drone" || enemyData.Name == "Robot")) // Movendo para a esquerda
            {
                sr.flipX = false;
            }
            else if(movement.x > 0 && (enemyData.Name == "Drone" || enemyData.Name == "Robot")) // Movendo para a direita
            {
                sr.flipX = true;
            }
            
        }           
    }

    public void Knockback(Vector2 velocity, float duration) // Método de recuo
    {
        if(knockbackDuration > 0)
        {
            return;
        }

        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}

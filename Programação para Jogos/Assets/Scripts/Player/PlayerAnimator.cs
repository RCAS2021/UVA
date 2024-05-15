using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Referências
    Animator am; // Componente Animator
    PlayerMovement pm; // Movimentação do jogador
    SpriteRenderer sr; // Componente SpriteRenderer


    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(pm.moveDir.x != 0 || pm.moveDir.y != 0) // Checa se x ou y do moveDir não é 0, então o jogador está se movendo
        {
            am.SetBool("Move", true);

            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if(pm.lastHorizontalVector < 0) // Checa se o jogador está virado para a esquerda
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}

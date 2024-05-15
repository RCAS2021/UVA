using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health; // Vida

    public void TakeDamage(float dmg) // Recebe o dano
    {
        health -= dmg; // Diminui a vida baseado no dano

        if(health <= 0) // Se a vida chegar a 0 ou menos, chama o método Kill()
        {
            Kill();
        }
    }

    public void Kill() // Destrói o objeto
    {
        Destroy(gameObject);
    }
}

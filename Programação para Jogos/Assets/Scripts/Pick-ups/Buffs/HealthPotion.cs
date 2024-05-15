using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup
{

    public int healthToRestore; // Quantidade de vida a ser restaurada

    public override void Collect() // Quando é coletado
    {
        if(hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore); // Restaura a vida do jogador pela quantidade 
    }
}

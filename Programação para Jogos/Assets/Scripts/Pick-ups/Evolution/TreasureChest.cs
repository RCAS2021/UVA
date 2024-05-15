using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    InventoryManager inventory;
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {       
        if(col.gameObject.CompareTag("Player"))
        {
            if(inventory.GetPossibleEvolutions().Count <= 0) // Se não existirem possíveis soluções, não destrói o baú
            {
                Debug.LogWarning("No available evolutions");
                return;
            }
            
            OpenTreasureChest();
            Destroy(gameObject);
        }
    }

    public void OpenTreasureChest()
    {
        if(inventory.GetPossibleEvolutions().Count <= 0) // Se não existirem possíveis evoluções, não realiza a função
        {
            Debug.LogWarning("No available evolutions");
            return;
        }

        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolutions()[Random.Range(0, inventory.GetPossibleEvolutions().Count)]; // Recebe uma evolução aleatória
        inventory.EvolveWeapon(toEvolve); // Chama o método e passa a evolução aleatória recebida
    }
}

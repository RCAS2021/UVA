using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name; // Nome do item
        public GameObject itemPrefab; // Prefab do item
        public float dropRate; // Chance de queda
    }

    public List<Drops> drops; // Lista de itens

    void OnDestroy()
    {
        if(!gameObject.scene.isLoaded)
        {
            return;
        }
        
        float randomNumber = UnityEngine.Random.Range(0f, 100f); // Gera um número de 0 a 100
        List<Drops> possibleDrops = new List<Drops>(); // Lista de possíveis itens

        foreach(Drops rate in drops) // Quando o número gerado é menor ou igual à porcentagem da chance de queda, o item será gerado
        {
            if(randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }
        
        if(possibleDrops.Count > 0) // Checa se existem possíveis itens
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }

}

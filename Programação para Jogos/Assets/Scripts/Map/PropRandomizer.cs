using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{

    public List<GameObject> propSpawnPoints; // Possíveis pontos para gerar os props
    public List<GameObject> propPrefabs; // Prefabs dos props

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach(GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count); // Gera um número aleatório de 0 até o número de Prefabs de props
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity); // Atribui um objeto de jogo ao prop recém gerado e instancia seu Prefab
            prop.transform.parent = sp.transform; // Referência o objeto de jogo pai e o define ao transform do ponto de geração atual, fazendo com que o prop gerado esteja referente à localização
        }
    }
}

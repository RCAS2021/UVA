using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{   
    MapController mc; // Controladora do mapa
    public GameObject targetMap; // Mapa alvo

    void Start()
    {
        mc = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D col) // Se o jogador estiver no chunk, o controlador recebe o chunk atual
    {
        if(col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col) // Quanto o jogador sair do chunk, altera o chunk atual da controladora
    {
        if(col.CompareTag("Player"))
        {
            if(mc.currentChunk == targetMap)
            {
                mc.currentChunk = null;
            }
        }
    }
}

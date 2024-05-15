using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player; // Dados do jogador
    public PassiveItemScriptableObject passiveItemData; // Dados do ScriptableObject do item passivo

    protected virtual void ApplyModifier()
    {
        // Aplica o multiplicador à estatística nas classes filhas
    }
    
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();

        ApplyModifier();
    }
}

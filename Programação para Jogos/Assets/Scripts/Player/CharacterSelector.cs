using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance; // Instância do seletor (singleton)
    public CharacterScriptableObject characterData; // Dados do ScriptableObject do personagem

    void Awake()
    {
        if(instance == null) // Singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Não destrói o objeto ao carregar uma nova cena
        }
        else
        {
            Debug.LogWarning("EXTRA" + this +" DELETED");
            Destroy(gameObject);
        }
    }
    
    public static CharacterScriptableObject GetData() // Recebe os dados do personagem
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterScriptableObject character) // Recebe o personagem escolhido
    {
        characterData = character;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}

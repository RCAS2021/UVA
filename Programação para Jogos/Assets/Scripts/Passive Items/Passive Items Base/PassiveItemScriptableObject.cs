using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier; // Multiplicador
    public float Multiplier { get => multiplier; private set => multiplier = value; }

    [SerializeField]
    int level; // Nível
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab; // Prefab do próximo nível
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    new string name; // Nome
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    string description; // Descrição
    public string Description { get => description; private set => description = value; }

    [SerializeField]
    Sprite icon; // Ícone
    public Sprite Icon { get => icon; private set => icon = value; }
}

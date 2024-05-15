using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab; // Prefab
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    // Base stats
    [SerializeField]
    float damage; // Dano
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed; // Velocidade
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration; // Duração do tempo de recarga
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    int pierce; // Penetração
    public int Pierce { get => pierce; private set => pierce = value; }

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

    [SerializeField]
    int evolvedUpgradeToRemove; // Arma a ser removida (quanto evoluída)
    public int EvolvedUpgradeToRemove { get => evolvedUpgradeToRemove; private set => evolvedUpgradeToRemove = value; }
}

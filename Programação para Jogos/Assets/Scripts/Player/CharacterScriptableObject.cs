using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon; // Arma inicial
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField]
    float maxHealth; // Vida máxima
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    
    [SerializeField]
    float recovery; // Regeneração de vida
    public float Recovery { get => recovery; private set => recovery = value; }
    
    [SerializeField]
    float moveSpeed; // Velocidade de movimento
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float might; // Força
    public float Might { get => might; private set => might = value; }

    [SerializeField]
    float projectileSpeed; // Velocidade de projétil
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    float magnet; // Imã
    public float Magnet { get => magnet; private set => magnet = value; }

    [SerializeField]
    Sprite icon; // Ícone
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    new string name; // Nome
    public string Name { get => name; private set => name = value; }
}

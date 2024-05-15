using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject 
{
    //ScriptableObject dos inimigos
    // Estatísticas base
    [SerializeField]
    float moveSpeed; // Velocidade de movimento
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float maxHealth; // Vida máxima
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float damage; // Dano
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    new string name; // Nome
    public string Name { get => name; private set => name = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData; // Dados do ScriptableObject da arma
    float currentCooldown; // Tempo de recarga atual
    protected PlayerMovement pm; // Movimentação do personagem

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }


    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime; // Decresce o tempo de recarga
        if(currentCooldown <= 0f) // Se o tempo de recarga chegar a 0, ataca
        {
            Attack();
        }
    }

    protected virtual void Attack() // Ataca e reinicia o tempo de recarga
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}

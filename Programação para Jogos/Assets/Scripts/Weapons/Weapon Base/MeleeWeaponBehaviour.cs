using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData; // Dados da arma
    public float destroyAfterSeconds; // Destrói após segundos

    // Current Stats
    protected float currentDamage; // Dano atual
    protected float currentSpeed; // Velocidade atual
    protected float currentCooldownDuration; // Duração do tempo de recarga atual
    protected int currentPierce; // Penetração atual

    void Awake() // Inicializa antes do start
    {
        // Dados atuais recebem os valores do ScriptableObject
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds); // Destrói o objeto após os segundos estabelecidos
    }

    public float GetCurrentDamage() // Atualiza o dano em relação à força
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) // Quando um inimigo ou objeto entra na área da arma e chama os respectivos métodos de dano
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
        }
        else if(col.CompareTag("Prop")) 
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }
}

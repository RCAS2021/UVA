using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies; // Lista de inimigos marcados (previne que inimigos recebam mais de uma instância de dano se entrarem e sairem do collider)
    public ParticleSystem auraEffect; 

    protected override void Start()
    {
        base.Start();
        if(auraEffect) Instantiate(auraEffect, transform.position, Quaternion.identity);
        markedEnemies = new List<GameObject>();
        currentDamage = GetCurrentDamage();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject)) // Se for um inimigo e não estiver marcado, causa dano e adiciona à lista de inimigos marcados
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, transform.position);

            markedEnemies.Add(col.gameObject);
        }

        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(col.gameObject)) // Se for um prop e não estiver marcado, causa dano e adiciona à lista de inimigos marcados
            {
                breakable.TakeDamage(currentDamage);

                markedEnemies.Add(col.gameObject);
            }
        }
    }
}

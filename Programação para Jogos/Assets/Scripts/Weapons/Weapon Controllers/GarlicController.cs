using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab); // Define um objeto de jogo para a arma e instancia o Prefab
        spawnedGarlic.transform.position = transform.position; // Move o objeto de jogo
        spawnedGarlic.transform.parent = transform;
    }
}

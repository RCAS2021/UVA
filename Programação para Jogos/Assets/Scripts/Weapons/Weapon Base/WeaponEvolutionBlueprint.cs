using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponEvolutionBlueprint", menuName = "ScriptableObjects/WeaponEvolutionBlueprint")]
public class WeaponEvolutionBlueprint : ScriptableObject
{
    public WeaponScriptableObject baseWeaponData; // Dados do ScriptableObject da arma base
    public PassiveItemScriptableObject catalystPassiveItemData; // Dados do ScriptableObject do item passivo base
    public WeaponScriptableObject evolvedWeaponData; // Dados do ScriptableObject da arma evoluída
    public GameObject evolvedWeapon; // Objeto de jogo da arma evoluída
}

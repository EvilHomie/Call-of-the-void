using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons;

    float minEnergyDmg = 10f;
    float maxEnergyDmg = 20f;

    float minKeneticDmg = 10f;
    float maxKeneticDmg = 20f;

    private void Awake()
    {
        GameObject weapon = Instantiate(weapons[Random.Range(0, weapons.Count)], transform);

        weapon.AddComponent<ShootIfPlayerInFocus>();

        float enDmg = Random.Range(minEnergyDmg, maxEnergyDmg);
        float kenDmg = Random.Range(minKeneticDmg, maxKeneticDmg);
        weapon.GetComponent<IWeapon>().SetParameters(enDmg, kenDmg);

    }
}

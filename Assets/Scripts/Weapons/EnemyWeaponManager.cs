using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    [SerializeField] List<GameObject> availableWeapons;
    [SerializeField] GameObject weaponContainer;

    [SerializeField] float minFireRate;
    [SerializeField] float maxFireRate;

    [SerializeField] float minProjectileSpeed;
    [SerializeField] float maxProjectileSpeed;

    [SerializeField] float minProjectileLifeTime;
    [SerializeField] float maxProjectileLifeTime;

    [SerializeField] float minEnergyDmg;
    [SerializeField] float maxEnergyDmg;

    [SerializeField] float minKeneticDmg;
    [SerializeField] float maxKeneticDmg;

    [SerializeField] float minWeaponDistance;
    [SerializeField] float maxWeaponDistance;

    private void Awake()
    {
        float fireRate = Random.Range(minFireRate, maxFireRate);
        float projectileSpeed = Random.Range(minProjectileSpeed, maxProjectileSpeed);
        float projectileLifeTime = Random.Range(minProjectileLifeTime, maxProjectileLifeTime);
        float enDmg = Random.Range(minEnergyDmg, maxEnergyDmg);
        float kenDmg = Random.Range(minKeneticDmg, maxKeneticDmg);
        float weaponDistance = Random.Range(minWeaponDistance, maxWeaponDistance);

        int randomIndex = Random.Range(0, availableWeapons.Count);
        GameObject weapon = Instantiate(availableWeapons[randomIndex], weaponContainer.transform);
        weapon.AddComponent<ShootInPlayer>().weaponDistance = weaponDistance;

        

        weapon.GetComponent<IWeapon>().SetWeaponParameters(fireRate, projectileSpeed, projectileLifeTime, enDmg, kenDmg, weaponDistance);
    }
}

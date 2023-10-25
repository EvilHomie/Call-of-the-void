using System.Collections.Generic;
using UnityEngine;

public class EnemyShipParametersManager : MonoBehaviour
{
    [SerializeField] float percentRangeMultipler = 10;

    [Header("Hp Parameters")]    
    [SerializeField] float defHullHp;
    [SerializeField] float defHullRegRate;
    [SerializeField] float defArmorHp;
    [SerializeField] float defArmorRegRate;
    [SerializeField] float defShieldHp;
    [SerializeField] float defShieldRegRate;
    [SerializeField] float shieldStartRegDelay;

    [Header("Weapon Parameters")]
    [SerializeField] List<GameObject> availableWeapons;
    [SerializeField] GameObject weaponContainer;
    [SerializeField] float defFireRate;
    [SerializeField] float defProjectileSpeed;
    [SerializeField] float defProjectileLifeTime;
    [SerializeField] float weaponDistance;
    [SerializeField] float defKineticDMG;
    [SerializeField] float defEnergyDMG;
    [SerializeField] float defAsteroidMultiplier;
    [SerializeField] float defEnemyMultiplier;

    [Header("Control Parameters")]
    [SerializeField] float defMainEngineSpeed;
    [SerializeField] float defRotateSpeed;
    [SerializeField] float defRCSSpeed;
    [SerializeField] float controlZoneRadius; 
    [SerializeField] float checkControlZoneDelay;

    private void Awake()
    {        
        SetHullParameters();
        GreatWeapon();
        SetControlParameters();
    }    

    private void SetHullParameters()
    {
        IDadamageable parameters = GetComponent<IDadamageable>();
        parameters.SetMaxHpParameters(
            ValueCalc(defHullHp),
            ValueCalc(defArmorHp),
            ValueCalc(defShieldHp));

        parameters.SetRegRates(
            ValueCalc(defHullRegRate),
            ValueCalc(defArmorRegRate),
            ValueCalc(defShieldRegRate),
            ValueCalc(shieldStartRegDelay));
    }

    void GreatWeapon()
    {
        float projectileSpeed = ValueCalc(defProjectileSpeed);
        float projectileLifeTime = ValueCalc(defProjectileLifeTime);
        weaponDistance = projectileSpeed * projectileLifeTime;
        controlZoneRadius = weaponDistance * 2;

        int randomWeaponIndex = Random.Range(0, availableWeapons.Count);
        GameObject weapon = Instantiate(availableWeapons[randomWeaponIndex], weaponContainer.transform);
        weapon.AddComponent<EnemyWeaponShootInPlayerLogic>().weaponDistance = weaponDistance;

        weapon.GetComponent<IWeapon>().SetWeaponParameters(
            ValueCalc(defFireRate),
            projectileSpeed,
            projectileLifeTime,
            ValueCalc(defEnergyDMG),
            ValueCalc(defKineticDMG),
            weaponDistance,
            defAsteroidMultiplier,
            defEnemyMultiplier);
    }

    private void SetControlParameters()
    {
        EnemyShipControlLogic controlLogic = GetComponent<EnemyShipControlLogic>();

        controlLogic.SetParameters(
            ValueCalc(defMainEngineSpeed),
            ValueCalc(defRotateSpeed),
            ValueCalc(defRCSSpeed),
            ValueCalc(controlZoneRadius),
            weaponDistance,
            checkControlZoneDelay
            );
    }

    float ValueCalc(float incomingValue)
    {
        float randomMod = Random.Range(-percentRangeMultipler, percentRangeMultipler) / 100;        
        float value = incomingValue * GlobalData.EnemyStrengthMultiplier - incomingValue * randomMod;
        return value;        
    }
}

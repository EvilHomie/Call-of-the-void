using UniRx;
using UnityEngine;

public class AsteroidParameters : MonoBehaviour, IDadamageable, ITarget
{
    [SerializeField] FloatReactiveProperty AsteroidHP;
    [SerializeField] FloatReactiveProperty AsteroidMaxHP;    

    void Start()
    {
        AsteroidHP.Value = AsteroidMaxHP.Value;
    }
    public void Damage(float energyDMG, float kineticDMG, float asteroidMultiplier, float enemyMultiplier)
    {
        AsteroidHP.Value -= (energyDMG + kineticDMG) * asteroidMultiplier;

        if (AsteroidHP.Value < 0)
        {
            EventBus.ComandOnObjDie.Execute(gameObject);
        }
    }
    public void GetCurrentParameters(out FloatReactiveProperty HullHPRP, out FloatReactiveProperty ArmorHPRP, out FloatReactiveProperty ShieldHPRP)
    {
        HullHPRP = AsteroidHP;
        ArmorHPRP = new();
        ShieldHPRP = new();
    }
    public void GetStaticParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP, out string name)
    {
        maxHullHP = AsteroidMaxHP.Value;
        maxArmorHP = 0;
        maxShieldHP = 0;
        name = gameObject.name;
    }

    public void SetMaxHpParameters(float hullHP, float armorHP, float shieldHP)
    {
        AsteroidMaxHP.Value = hullHP ;
    }

    public void SetRegRates(float hullRegRate, float armorRegRate, float shieldRegRate, float shieldStartRegDelay)
    {
    }
}

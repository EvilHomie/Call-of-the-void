using UniRx;
using UnityEngine;

public class AsteroidParameters : MonoBehaviour, IDadamageable, ITarget
{
    [SerializeField] FloatReactiveProperty asteroidHP;
    [SerializeField] float asteroidMaxHP;    

    public void Damage(float energyDMG, float kineticDMG)
    {
        asteroidHP.Value -= energyDMG + kineticDMG;

        if (asteroidHP.Value < 0)
        {
            EventBus.ComandOnObjDie.Execute(gameObject);
        }
    }
    public void GetCurrentParameters(out FloatReactiveProperty HullHPRP, out FloatReactiveProperty ArmorHPRP, out FloatReactiveProperty ShieldHPRP)
    {
        HullHPRP = asteroidHP;
        ArmorHPRP = new();
        ShieldHPRP = new();
    }
    public void GetStaticParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP, out string name)
    {
        maxHullHP = asteroidMaxHP;
        maxArmorHP = 0;
        maxShieldHP = 0;
        name = gameObject.name;
    }

    public void SetParameters(float hp)
    {
        asteroidHP.Value = hp;
        asteroidMaxHP = hp;
    }
}

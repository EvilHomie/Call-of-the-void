using UnityEngine;

public class AsteroidParameters : MonoBehaviour, IDadamageable, ITarget
{
    [SerializeField] float asteroidHP;
    [SerializeField] float asteroidMaxHP;    

    public void Damage(float energyDMG, float kineticDMG)
    {
        asteroidHP -= energyDMG + kineticDMG;

        if (asteroidHP < 0)
        {            
            EventBus.onObjDie?.Invoke(gameObject);
        }
    }
    public void GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP)
    {
        hullHP = asteroidHP;
        armorHP = 0;
        shieldHP = 0;
    }
    public void GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP)
    {
        maxHullHP = asteroidMaxHP;
        maxArmorHP = 0;
        maxShieldHP = 0;
    }

    public void SetParameters(float hp)
    {
        asteroidHP = hp;
        asteroidMaxHP = hp;
    }
}

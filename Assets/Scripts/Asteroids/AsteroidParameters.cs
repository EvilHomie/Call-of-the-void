using UnityEngine;

public class AsteroidParameters : MonoBehaviour, IDadamageable, ITarget
{
    ObjectManager objectManager;
    [SerializeField] float asteroidHP;
    [SerializeField] float asteroidMaxHP;

    private void Awake()
    {
        asteroidHP = GetComponent<Rigidbody>().mass;
        objectManager = GetComponent<ObjectManager>();
        asteroidMaxHP = asteroidHP;
    }

    public void Damage(float energyDMG, float kineticDMG)
    {
        asteroidHP -= energyDMG + kineticDMG;

        if (asteroidHP < 0)
        {            
            objectManager.OnDestroyEvents();
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
}

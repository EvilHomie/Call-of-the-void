using System.Collections;
using UnityEngine;

public class StationParameters : MonoBehaviour, IDadamageable, ITargetParameters
{
    [SerializeField] float hullHP;
    [SerializeField] float armorHP;
    [SerializeField] float shieldHP;

    float fullHull;
    float fullArmor;
    float fullShield;

    [SerializeField] float hullRegRate;
    [SerializeField] float armorRegRate;
    [SerializeField] float shieldRegRate;
    Coroutine disableShield;

    [SerializeField] float shieldStartRegDelay;
    [SerializeField] bool shieldActive = true;

    private void Awake()
    {
        fullHull = hullHP;
        fullArmor = armorHP;
        fullShield = shieldHP;
        
    }
    private void FixedUpdate()
    {
        RegShield();
    }    

    void RegShield()
    {
        if (shieldActive & shieldHP < fullShield)
        {
            shieldHP += shieldRegRate * Time.fixedDeltaTime;
        }
    }

    public void Damage(float energyDMG, float kineticDMG)
    {     
        shieldHP -= energyDMG;

        if (shieldHP <= 0)
        {
            CheckIfShieldDisable();
            shieldHP = 0;            

            armorHP -= kineticDMG;

            if (armorHP <= 0)
            {
                armorHP = 0;
                hullHP -= energyDMG + kineticDMG;

                if (hullHP <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }     
    }

    public void GetTargetParameters(out float hullHP, out float armorHP, out float shieldHP)
    {
        hullHP = this.hullHP;
        armorHP = this.armorHP;
        shieldHP = this.shieldHP;
    }

    void CheckIfShieldDisable()
    {
        if (shieldActive)
        {
            shieldActive = false;
            disableShield = StartCoroutine(DisableShield());
        }
        else if (!shieldActive)
        {
            StopCoroutine(disableShield);
            disableShield = StartCoroutine(DisableShield());
        }
    }
    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(shieldStartRegDelay);
        shieldActive = true;
    }

    public void GetTargetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP)
    {
        maxHullHP = fullHull;
        maxArmorHP = fullArmor;
        maxShieldHP = fullShield;
    }
}

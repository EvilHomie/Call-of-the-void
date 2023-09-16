using System;
using System.Collections;
using UnityEngine;

public class Parameters : MonoBehaviour, IDadamageable, ITargetParameters
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
    [SerializeField] bool shieldIsActive = true;

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
        if (shieldIsActive & shieldHP < fullShield)
        {
            shieldHP += shieldRegRate * Time.fixedDeltaTime;
        }
    }

    public void Damage(float energyDMG, float kineticDMG)
    {     
        shieldHP -= energyDMG;
        

        if (shieldHP <= 0)
        {
            shieldHP = 0;
            CheckIfShieldStatus();            
        }     

        if (!shieldIsActive)
        {
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
    void CheckIfShieldStatus()
    {
        if (shieldIsActive)
        {
            disableShield = StartCoroutine(DisableShieldOnDelay());
        }
        else if (!shieldIsActive)
        {
            StopCoroutine(disableShield);
            disableShield = StartCoroutine(DisableShieldOnDelay());
        }
    }
    IEnumerator DisableShieldOnDelay()
    {
        shieldIsActive = false;
        yield return new WaitForSeconds(shieldStartRegDelay);
        shieldIsActive = true;
    }

    public void GetTargetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP)
    {
        maxHullHP = fullHull;
        maxArmorHP = fullArmor;
        maxShieldHP = fullShield;
    }
}

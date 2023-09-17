using System;
using System.Collections;
using UnityEngine;

public class Parameters : MonoBehaviour, IDadamageable, ITarget
{
    [SerializeField] ParticleSystem[] explodeParticle;
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
            CheckShieldStatus();            
        }     

        if (!shieldIsActive)
        {
            armorHP -= kineticDMG;

            if (armorHP <= 0)
            {
                armorHP = 0;
                hullHP -= energyDMG + kineticDMG;                
            }
        }
        if (hullHP <= 0)
        {
            StartCoroutine(Destroy());
        }
    }
    public void GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP)
    {
        maxHullHP = fullHull;
        maxArmorHP = fullArmor;
        maxShieldHP = fullShield;
    }
    public void GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP)
    {
        hullHP = this.hullHP;
        armorHP = this.armorHP;
        shieldHP = this.shieldHP;
    }
    void CheckShieldStatus()
    {
        if (shieldIsActive)
        {
            disableShield = StartCoroutine(DisableShieldOnDelay());
        }
        else
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

    IEnumerator Destroy()
    {
        explodeParticle[0].gameObject.SetActive(true);
        if (explodeParticle.Length > 1)
        {
            yield return new WaitForSeconds(1);
            explodeParticle[1].gameObject.SetActive(true);

            if (explodeParticle.Length > 2)
            {
                yield return new WaitForSeconds(1);
                explodeParticle[2].gameObject.SetActive(true);
                yield return new WaitForSeconds(explodeParticle[2].main.duration);
                Destroy(gameObject);
            }
        }
        else
        {
            yield return new WaitForSeconds(explodeParticle[0].main.duration);
            Destroy(gameObject);
        }  
    }
}

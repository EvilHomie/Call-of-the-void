using System.Collections;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Parameters : MonoBehaviour, IDadamageable, ITarget
{
    [SerializeField] FloatReactiveProperty HullHP = new();
    [SerializeField] FloatReactiveProperty ArmorHP = new();
    [SerializeField] FloatReactiveProperty ShieldHP = new();

    [SerializeField] FloatReactiveProperty fullHull;
    [SerializeField] FloatReactiveProperty fullArmor;
    [SerializeField] FloatReactiveProperty fullShield;

    [SerializeField] float hullRegRate;
    [SerializeField] float armorRegRate;
    [SerializeField] float shieldRegRate;
    Coroutine disableShield;

    [SerializeField] float shieldStartRegDelay;
    [SerializeField] bool shieldIsActive = true;
    [SerializeField] bool objDie = false;

    void Start()
    {
        HullHP.Value = fullHull.Value;
        ArmorHP.Value = fullArmor.Value;
        ShieldHP.Value = fullShield.Value;
    }
    private void FixedUpdate()
    {
        Regeneration();
    }

    void Regeneration()
    {
        if (shieldIsActive && ShieldHP.Value < fullShield.Value)
        {
            ShieldHP.Value += shieldRegRate * Time.fixedDeltaTime;
        }
    }

    public void Damage(float energyDMG, float kineticDMG, float asteroidMultiplier, float enemyMultiplier)
    {
        ShieldHP.Value -= energyDMG * enemyMultiplier;

        if (ShieldHP.Value <= 0)
        {
            ShieldHP.Value = 0;
            CheckShieldStatus();
        }

        if (!shieldIsActive)
        {
            ArmorHP.Value -= kineticDMG * enemyMultiplier;

            if (ArmorHP.Value <= 0)
            {
                ArmorHP.Value = 0;
                HullHP.Value -= (energyDMG + kineticDMG) * enemyMultiplier;
            }
        }
        if (HullHP.Value <= 0 && !objDie)
        {
            objDie = true;
            EventBus.CommandOnObjDie.Execute(gameObject);
        }


        if (gameObject.CompareTag("Player"))
        {
            EventBus.CommandOnPlayerTakeDamage.Execute();
            Debug.Log("PLayerTakeDamage");
        }
    }
    
    void CheckShieldStatus()
    {
        if (shieldIsActive)
        {
            shieldIsActive = false;
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
        yield return new WaitForSeconds(shieldStartRegDelay);
        shieldIsActive = true;
    }

    #region IDadamageable
    public void SetMaxHpParameters(float hullHP, float armorHP, float shieldHP)
    {
        fullHull.Value = hullHP;
        fullArmor.Value = armorHP;
        fullShield.Value = shieldHP;
    }

    public void SetRegRates(float hullRegRate, float armorRegRate, float shieldRegRate, float shieldStartRegDelay)
    {
        this.hullRegRate = hullRegRate;
        this.armorRegRate = armorRegRate;
        this.shieldRegRate = shieldRegRate;
        this.shieldStartRegDelay = shieldStartRegDelay;
    }
    #endregion

    #region ITarget
    public void GetStaticParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP, out string name)
    {
        maxHullHP = fullHull.Value;
        maxArmorHP = fullArmor.Value;
        maxShieldHP = fullShield.Value;
        name = gameObject.name;
    }
    public void GetCurrentParameters(out FloatReactiveProperty HullHPRP, out FloatReactiveProperty ArmorHPRP, out FloatReactiveProperty ShieldHPRP)
    {

        HullHPRP = HullHP;
        ArmorHPRP = ArmorHP;
        ShieldHPRP = ShieldHP;
    }
    #endregion

}







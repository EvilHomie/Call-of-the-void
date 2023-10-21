using System.Collections;
using UniRx;
using UnityEngine;

public class Parameters : MonoBehaviour, IDadamageable, ITarget
{
    [SerializeField] FloatReactiveProperty HullHP = new();
    [SerializeField] FloatReactiveProperty ArmorHP = new();
    [SerializeField] FloatReactiveProperty ShieldHP = new();

    float fullHull;
    float fullArmor;
    float fullShield;

    [SerializeField] float hullRegRate;
    [SerializeField] float armorRegRate;
    [SerializeField] float shieldRegRate;
    Coroutine disableShield;

    [SerializeField] float shieldStartRegDelay;
    [SerializeField] bool shieldIsActive = true;
    [SerializeField] bool objDie = false;

    private void Awake()
    {
        fullHull = HullHP.Value;
        fullArmor = ArmorHP.Value;
        fullShield = ShieldHP.Value;
    }
    private void FixedUpdate()
    {
        RegShield();
    }

    void RegShield()
    {
        if (shieldIsActive & ShieldHP.Value < fullShield)
        {
            ShieldHP.Value += shieldRegRate * Time.fixedDeltaTime;
        }
    }

    public void Damage(float energyDMG, float kineticDMG)
    {
        ShieldHP.Value -= energyDMG;

        if (ShieldHP.Value <= 0)
        {
            ShieldHP.Value = 0;
            CheckShieldStatus();
        }

        if (!shieldIsActive)
        {
            ArmorHP.Value -= kineticDMG;

            if (ArmorHP.Value <= 0)
            {
                ArmorHP.Value = 0;
                HullHP.Value -= energyDMG + kineticDMG;
            }
        }
        if (HullHP.Value <= 0 && !objDie)
        {
            objDie = true;
            EventBus.ComandOnObjDie.Execute(gameObject);
        }


        if (gameObject.CompareTag("Player"))
        {
            EventBus.ComandOnPlayerTakeDamage.Execute();
            Debug.Log("PLayerTakeDamage");
        }
    }
    public void GetStaticParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP, out string name)
    {
        maxHullHP = fullHull;
        maxArmorHP = fullArmor;
        maxShieldHP = fullShield;
        name = gameObject.name;
    }
    public void GetCurrentParameters(out FloatReactiveProperty HullHPRP, out FloatReactiveProperty ArmorHPRP, out FloatReactiveProperty ShieldHPRP)
    {

        HullHPRP = HullHP;
        ArmorHPRP = ArmorHP;
        ShieldHPRP = ShieldHP;
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


}
//    if (shieldHP.Value > 0)
//        {
//            shieldHP.Value -= energyDMG;
//            if (shieldHP.Value< 0)
//            {
//                shieldHP.Value = 0;
//                StartCoroutine(DisableShieldOnDelay());
//}
                
//        }
            





//        if (shieldHP.Value == 0)
//    armorHP.Value -= kineticDMG;





//if (shieldHP.Value == 0 && armorHP.Value == 0)
//    hullHP.Value -= energyDMG + kineticDMG;











//if (shieldHP.Value > 0)
//    shieldHP.Value -= energyDMG;

//if (shieldHP.Value < 0)
//    shieldHP.Value = 0;


//if (shieldHP.Value <= 0)
//{
//    shieldHP.Value = 0;
//    CheckShieldStatus();
//}







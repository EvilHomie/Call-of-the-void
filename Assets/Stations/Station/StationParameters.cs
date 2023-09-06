using System;
using UnityEngine;

public class StationParameters : MonoBehaviour, IDadamageable, IDisplayTargetHP
{
    DisplayTargetParameters displayTargetParameters;


    public static Action<float, float, float> broadcastStationParameters;

    float hullHP = 100f;
    float armorHP = 100f;
    [SerializeField] float shieldHP = 100f;

    float fullHp = 100f;

    float hullRegRate = 0f;
    float armorRegRate = 0f;
    float shieldRegRate = 5f;


    private void Awake()
    {
        displayTargetParameters = FindFirstObjectByType<DisplayTargetParameters>();
    }
    private void FixedUpdate()
    {
        ParametersUpdate();
        RegShield();

        SendTargetObject();
    }

    void ParametersUpdate()
    {
        broadcastStationParameters?.Invoke(hullHP, armorHP, shieldHP);
    }    

    void RegShield()
    {
        if (shieldHP < fullHp)
        {
            shieldHP += shieldRegRate * Time.deltaTime;
        }        
    }

    public void Damage(float damageValue)
    {
        shieldHP -= damageValue;
        //PlayerTakeDamageEvents?.Invoke();
        //if (gameObject.CompareTag("Player"))
        //{
        //    displayTargetParameters.GetLastHitObject(gameObject.tag, hullHP, armorHP, shieldHP);
        //}        
    }

    public void SendTargetObject()
    {
        //broadcastStationParameters?.Invoke(hullHP, armorHP, shieldHP);
    }
}

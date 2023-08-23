using System;
using UnityEngine;

public class PlayerShipParameters : MonoBehaviour
{
    public static Action onTakeDamageEvents;

    public static Action<float, float, float> curentParameters;

    float hullHP = 100f;
    float armorHP = 100f;
    float shieldHP = 100f;

    float fullHp = 100f;

    float hullRegRate = 0f;
    float armorRegRate = 0f;
    float shieldRegRate = 5f;

    private void FixedUpdate()
    {
        ParametersUpdate();
        RegShield();
    }

    void ParametersUpdate()
    {
        curentParameters?.Invoke(hullHP, armorHP, shieldHP);
    }
    public void TakeDamage(float damage)
    {
        shieldHP -= damage;
        onTakeDamageEvents?.Invoke();
    }

    void RegShield()
    {
        if (shieldHP < fullHp)
        {
            shieldHP += shieldRegRate * Time.deltaTime;
        }        
    }
}

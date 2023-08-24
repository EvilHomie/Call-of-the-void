using System;
using UnityEngine;

public class PlayerShipParameters : MonoBehaviour, IDadamageable
{
    public static Action PlayerTakeDamageEvents;

    public static Action<float, float, float> broadcastPlayerParameters;

    float hullHP = 100f;
    float armorHP = 100f;
    [SerializeField] float shieldHP = 100f;

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
        broadcastPlayerParameters?.Invoke(hullHP, armorHP, shieldHP);
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
        PlayerTakeDamageEvents?.Invoke();
    }
}

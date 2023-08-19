using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipParameters : MonoBehaviour
{
    [SerializeField] Slider hullSlider;
    [SerializeField] Slider armorSlider;
    [SerializeField] Slider shieldSlider;
    [SerializeField] TextMeshProUGUI shieldHPText;

    public static Action<float, float, float> onTakeDamage;

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
        hullSlider.value = hullHP;
        armorSlider.value = armorHP;
        shieldSlider.value = shieldHP;

        shieldHPText.text = $"{Mathf.Round(shieldHP)} %";
    }
    public void TakeDamage(float damage)
    {
        shieldHP -= damage;
        onTakeDamage?.Invoke(2,2,1);
    }

    void RegShield()
    {
        if (shieldSlider.value < fullHp)
        {
            shieldHP += shieldRegRate * Time.deltaTime;
        }        
    }
}

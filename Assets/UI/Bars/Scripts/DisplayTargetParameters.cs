using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTargetParameters : MonoBehaviour
{
    [SerializeField] Slider hullSlider;
    [SerializeField] Slider armorSlider;
    [SerializeField] Slider shieldSlider;

    [SerializeField] TextMeshProUGUI hullHPText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI armorHPText;
    [SerializeField] TextMeshProUGUI shieldHPText;



    string gameObjectTag;
    float hullHp;
    float armorHp;
    float shieldHp;


    private void OnEnable()
    {
        BroadcastTargetParameters.maxParameters += SetMaxParameters;
        BroadcastTargetParameters.broadcastParameters += DisplayParameters;
    }

    private void OnDisable()
    {
        BroadcastTargetParameters.maxParameters += SetMaxParameters;
        BroadcastTargetParameters.broadcastParameters -= DisplayParameters;

    }
    void DisplayParameters(string name, float hullHP, float armorHP, float shieldHP)
    {
        hullSlider.value = hullHP;
        armorSlider.value = armorHP;
        shieldSlider.value = shieldHP;

        nameText.text = name;
        hullHPText.text = Mathf.Round(hullHP).ToString();
        armorHPText.text = Mathf.Round(armorHP).ToString();
        shieldHPText.text = Mathf.Round(shieldHP).ToString();
    }
    void SetMaxParameters(float maxHullHP, float maxArmorHP, float maxShieldHP)
    {
        hullSlider.maxValue = maxHullHP;
        armorSlider.maxValue = maxArmorHP;
        shieldSlider.maxValue = maxShieldHP;
        Debug.Log("wad");
    }
}

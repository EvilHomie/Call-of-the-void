using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayParameters : MonoBehaviour
{
    Slider hullSlider;
    Slider armorSlider;
    Slider shieldSlider;

    TextMeshProUGUI hullHPText;
    TextMeshProUGUI nameText;
    TextMeshProUGUI armorHPText;
    TextMeshProUGUI shieldHPText;

    private void Awake()
    {
        GetReferences();
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected void DisplayCurrentParameters(float hullHP, float armorHP, float shieldHP)
    {
        hullSlider.value = hullHP;
        armorSlider.value = armorHP;
        shieldSlider.value = shieldHP;
        
        hullHPText.text = Mathf.Round(hullHP).ToString();
        armorHPText.text = Mathf.Round(armorHP).ToString();
        shieldHPText.text = Mathf.Round(shieldHP).ToString();
    }
    protected void SetMaxParameters(string name, float maxHullHP, float maxArmorHP, float maxShieldHP)
    {
        nameText.text = name;        
        hullSlider.maxValue = maxHullHP;
        armorSlider.maxValue = maxArmorHP;
        shieldSlider.maxValue = maxShieldHP;
    }

    void GetReferences()
    {
        hullSlider = transform.Find("Hull Slider").GetComponent<Slider>();
        armorSlider = transform.Find("Armor Slider").GetComponent<Slider>();
        shieldSlider = transform.Find("Shield Slider").GetComponent<Slider>();

        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        hullHPText = hullSlider.transform.Find("Hull HP Text").GetComponent<TextMeshProUGUI>();
        armorHPText = armorSlider.transform.Find("Armor HP Text").GetComponent<TextMeshProUGUI>();
        shieldHPText = shieldSlider.transform.Find("Shield HP Text").GetComponent<TextMeshProUGUI>();
    }        
}

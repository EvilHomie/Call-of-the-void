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

    private void OnEnable()
    {
        PlayerTargetManager.targetMaxParameters += SetMaxParameters;
        PlayerTargetManager.broadcastTargetParameters += DisplayParameters;
    }

    private void OnDisable()
    {
        PlayerTargetManager.targetMaxParameters -= SetMaxParameters;
        PlayerTargetManager.broadcastTargetParameters -= DisplayParameters;

    }
    void DisplayParameters(float hullHP, float armorHP, float shieldHP)
    {
        hullSlider.value = hullHP;
        armorSlider.value = armorHP;
        shieldSlider.value = shieldHP;
        
        hullHPText.text = Mathf.Round(hullHP).ToString();
        armorHPText.text = Mathf.Round(armorHP).ToString();
        shieldHPText.text = Mathf.Round(shieldHP).ToString();
    }
    void SetMaxParameters(string name, float maxHullHP, float maxArmorHP, float maxShieldHP)
    {
        nameText.text = name;        
        hullSlider.maxValue = maxHullHP;
        armorSlider.maxValue = maxArmorHP;
        shieldSlider.maxValue = maxShieldHP;
    }
}

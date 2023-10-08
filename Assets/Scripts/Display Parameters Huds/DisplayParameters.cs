using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayParameters : MonoBehaviour
{
    [SerializeField] Slider hullSlider;
    [SerializeField] Slider armorSlider;
    [SerializeField] Slider shieldSlider;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI hullHPText;
    [SerializeField] TextMeshProUGUI armorHPText;
    [SerializeField] TextMeshProUGUI shieldHPText;

    ITarget targetParameter;   

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    private void FixedUpdate()
    {
        if (targetParameter != null)
        {            
            DisplayCurrentParameters();
        }        
    }
    protected void DisplayCurrentParameters()
    {
        targetParameter.GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP);
        
        hullSlider.value = hullHP;
        armorSlider.value = armorHP;
        shieldSlider.value = shieldHP;
        
        hullHPText.text = Mathf.Round(hullHP).ToString();
        armorHPText.text = Mathf.Round(armorHP).ToString();
        shieldHPText.text = Mathf.Round(shieldHP).ToString();

        if (hullHP <= 0)
        {
            TurnOffDisplay();
        }
    }
    protected void SetMaxParameters(GameObject obj)
    {
        if (obj != null)
        {
            SwitchBars(true);
            targetParameter = obj.GetComponent<ITarget>();
            targetParameter.GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
            nameText.text = obj.tag;
            hullSlider.maxValue = maxHullHP;
            armorSlider.maxValue = maxArmorHP;
            shieldSlider.maxValue = maxShieldHP;
        }        
    }

    protected void TurnOffDisplay()
    {
        targetParameter = null;
        SwitchBars(false);
    }
    protected void SwitchBars(bool status)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(status);
        }
    }
}

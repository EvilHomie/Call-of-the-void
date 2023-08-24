using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShipeParameters : MonoBehaviour
{
    [SerializeField] Slider hullSlider;
    [SerializeField] Slider armorSlider;
    [SerializeField] Slider shieldSlider;

    [SerializeField] TextMeshProUGUI hullHPText;
    [SerializeField] TextMeshProUGUI armorHPText;
    [SerializeField] TextMeshProUGUI shieldHPText;

    private void OnEnable()
    {
        PlayerShipParameters.broadcastPlayerParameters += DisplayParameters;
    }

    private void OnDisable()
    {
        PlayerShipParameters.broadcastPlayerParameters -= DisplayParameters;
    }

    void DisplayParameters(float hullHP, float armorHP, float shieldHP)
    {
        hullSlider.value = hullHP;
        armorSlider.value = armorHP;
        shieldSlider.value = shieldHP;

        hullHPText.text = $"{Mathf.Round(hullHP)} %";
        armorHPText.text = $"{Mathf.Round(armorHP)} %";
        shieldHPText.text = $"{Mathf.Round(shieldHP)} %";
    }
}

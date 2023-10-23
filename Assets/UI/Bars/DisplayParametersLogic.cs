using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DisplayParametersLogic : MonoBehaviour
{
    CompositeDisposable _disposables = new();

    [SerializeField] Slider hullSlider;
    [SerializeField] Slider armorSlider;
    [SerializeField] Slider shieldSlider;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI hullHPText;
    [SerializeField] TextMeshProUGUI armorHPText;
    [SerializeField] TextMeshProUGUI shieldHPText;

    protected void EnableTargetDisplay(GameObject target)
    {
        Debug.Log("EnableTargetDisplay");
        ITarget parameters = target.GetComponent<ITarget>();

        SetMaxParameters(parameters);
        SetSubscibesOnParameters(parameters);
        DisplayManager(true);
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }
    void SetMaxParameters(ITarget parameters)
    {
        Debug.Log("SetMaxParameters");
        _disposables.Clear();
        parameters.GetStaticParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP, out string name);

        nameText.text = name;

        SetSlider(hullSlider, hullHPText, maxHullHP);
        SetSlider(armorSlider, armorHPText, maxArmorHP);
        SetSlider(shieldSlider, shieldHPText, maxShieldHP);
    }

    void SetSubscibesOnParameters(ITarget parameters)
    {
        Debug.Log("SetSubscibesOnParameters");
        parameters.GetCurrentParameters(out FloatReactiveProperty hullHPRP, out FloatReactiveProperty armorHPRP, out FloatReactiveProperty shieldHPRP);

        hullHPRP.Subscribe(hullHp =>        
        {
            UpdateSliderValue(hullSlider, hullHPText, hullHp);
            if (hullHp <= 0) DesableTargetDisplay();
        }).AddTo(_disposables);

        armorHPRP.Subscribe(armorHP => UpdateSliderValue(armorSlider, armorHPText, armorHP)).AddTo(_disposables);

        shieldHPRP.Subscribe(shieldHP => UpdateSliderValue(shieldSlider, shieldHPText, shieldHP)).AddTo(_disposables);
    }

    void SetSlider(Slider slider, TextMeshProUGUI text, float value)
    {
        slider.maxValue = value;
        text.text = Mathf.Round(value).ToString();
    }

    void UpdateSliderValue(Slider slider, TextMeshProUGUI text, float value)
    {
        slider.value = value;
        text.text = Mathf.Round(value).ToString();
        Debug.Log($"{slider.name}      {value}");
    }


    protected void DesableTargetDisplay()
    {
        Debug.Log("DesableTargetDisplay");
        _disposables.Clear();
        DisplayManager(false);

    }

    void DisplayManager(bool status)
    {
        foreach (Transform sliders in transform)
        {
            sliders.gameObject.SetActive(status);
        }
    }
}

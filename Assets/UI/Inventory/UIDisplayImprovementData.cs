using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayImprovementData : MonoBehaviour
{
    CompositeDisposable _disposable_Device = new();
    CompositeDisposable _disposable_ImprovementLevel = new();
    [SerializeField] TextMeshProUGUI improvementNameText;
    [SerializeField] Transform improvementLevelIconContainer;
    [SerializeField] TextMeshProUGUI currentValueText;
    [SerializeField] TextMeshProUGUI upgEffectText;
    [SerializeField] TextMeshProUGUI maximumLevelReachedText;
    [SerializeField] Transform upgContainer;
    [SerializeField] Button upgButton;
    Improvement currentImprovement;
    ImprovementCost currentUpgradeCost;


    void OnEnable()
    {
        EventBus.SelectDevice.Where(device => device != null && CheckImprovementMatch(device)).Subscribe(device =>
        {
            _disposable_ImprovementLevel.Clear();
            DisplayConstantData();
            DisplayDinamicData();

        }).AddTo(_disposable_Device);
    }
    void OnDisable()
    {
        _disposable_Device.Clear();
        _disposable_ImprovementLevel.Clear();
    }

    bool CheckImprovementMatch(IDevice improvement)
    {
        if (transform.GetSiblingIndex() < improvement.GetImprovements().Count)
        {
            this.currentImprovement = improvement.GetImprovements()[transform.GetSiblingIndex()];
            return true;
        }
        else return false;
    }
    void DisplayConstantData()
    {
        improvementNameText.text = currentImprovement.improvementName;
        upgEffectText.text = $"+ {currentImprovement.upgEffect}";
    }
    void DisplayDinamicData()
    {
        currentImprovement.improvementLevel.Subscribe(level => UpdateDinamicData(level)).AddTo(_disposable_ImprovementLevel);
    }

    void UpdateDinamicData(int level)
    {
        DisplayLevelIcons(level);
        DisplayCurValue(level);
        DisplayUpgCondition(level);
    }

    void DisplayCurValue(int level)
    {
        currentValueText.text = (level * currentImprovement.upgEffect).ToString();
    }


    void DisplayLevelIcons(int level)
    {
        foreach (Transform icon in improvementLevelIconContainer)
        {
            if (icon.GetSiblingIndex() < level)
            {
                icon.GetComponent<RawImage>().color = Color.green;
            }
            else if (icon.GetSiblingIndex() >= level & icon.GetSiblingIndex() < currentImprovement.improvementMaxLevel)
            {
                icon.GetComponent<RawImage>().color = Color.grey;
            }
            else icon.GetComponent<RawImage>().color = Color.red;
        }
    }

    void DisplayUpgCondition(int level)
    {
        if (level < currentImprovement.improvementMaxLevel)
        {
            SwitchDisplayCondition(true);            
            DisplayUpgradeConditions(level);
        }
        else
        {
            SwitchDisplayCondition(false);
        }
    }

    void DisplayUpgradeConditions(int level)
    {
        currentUpgradeCost = EventBus.SelectDevice.Value.GetImprovementsCosts().Find(cost => cost.upgradeLevel == level + 1);
        DisplayEvalebleConditions();
    }

    void DisplayEvalebleConditions()
    {
        foreach (Transform conditionObj in upgContainer)
        {
            if (conditionObj.GetSiblingIndex() < currentUpgradeCost.conditionsForUpgrade.Count)
            {
                conditionObj.gameObject.SetActive(true);
                FillConditionData(conditionObj, currentUpgradeCost);
            }
            else conditionObj.gameObject.SetActive(false);
        }
    }

    void SwitchDisplayCondition(bool enabled)
    {
        maximumLevelReachedText.gameObject.SetActive(!enabled);
        upgContainer.gameObject.SetActive(enabled);
        upgEffectText.gameObject.SetActive(enabled);
        upgButton.gameObject.SetActive(enabled);
    }
    void FillConditionData(Transform conditionObj, ImprovementCost condition)
    {
        int index = conditionObj.GetSiblingIndex();

        conditionObj.GetComponentInChildren<TextMeshProUGUI>().text = $"x {condition.conditionsForUpgrade[index].resAmount}";
        conditionObj.GetComponentInChildren<Image>().sprite = EventBus.CommandOnGetResImageByName(condition.conditionsForUpgrade[index].resourceType.ToString());
    }

    public void UpgradeLevel()
    {
        if (EventBus.SpendResOnBuy?.Invoke(currentUpgradeCost.conditionsForUpgrade) == true)
        {
            EventBus.CommandForPlaySound.Execute("successSound");
            currentImprovement.improvementLevel.Value++;
        }
        else
        {
            EventBus.CommandForPlaySound.Execute("errorSound");
        }
    }
}
       

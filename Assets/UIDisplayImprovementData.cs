using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayImprovementData : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] TextMeshProUGUI improvementNameText;
    [SerializeField] Transform improvementLevelIconContainer;
    [SerializeField] TextMeshProUGUI currentValueText;
    [SerializeField] TextMeshProUGUI upgEffectText;
    [SerializeField] TextMeshProUGUI maximumLevelReachedText;
    [SerializeField] Transform upgContainer;
    [SerializeField] Button upgButton;
    [SerializeField] List<Sprite> resourceSprites;
    Improvement improvement;


    void OnEnable()
    {
        EventBus.SelectDevice.Where(device => device != null && TryGetImprovementMatch(device)).Subscribe(device =>
        {
            _disposable.Clear();
            DisplayConstantData();
            SubscribeOnLevelValue();

        }).AddTo(_disposable);
    }
    void OnDisable()
    {
        _disposable.Clear();
    }

    bool TryGetImprovementMatch(IDevice improvement)
    {
        if (transform.GetSiblingIndex() < improvement.GetImprovements().Count)
        {
            this.improvement = improvement.GetImprovements()[transform.GetSiblingIndex()];
            return true;
        }
        else return false;

    }
    void DisplayConstantData()
    {
        improvementNameText.text = improvement.improvementName;
        upgEffectText.text = $"+ {improvement.upgEffect}";
    }

    void SubscribeOnLevelValue()
    {
        improvement.improvementLevel.Subscribe(level => UpdateDinamicData(level)).AddTo(_disposable);
    }

    void UpdateDinamicData(int level)
    {
        DisplayLevelIcons(level);
        DisplayCurValue(level);
        DisplayUpgCondition(level);
    }

    void DisplayCurValue(int level)
    {
        currentValueText.text = (level * improvement.upgEffect).ToString();
    }


    void DisplayLevelIcons(int level)
    {
        foreach (Transform icon in improvementLevelIconContainer)
        {
            if (icon.GetSiblingIndex() < level)
            {
                icon.GetComponent<RawImage>().color = Color.green;
            }
            else if (icon.GetSiblingIndex() >= level & icon.GetSiblingIndex() < improvement.improvementMaxLevel)
            {
                icon.GetComponent<RawImage>().color = Color.grey;
            }
            else icon.GetComponent<RawImage>().color = Color.red;
        }
    }

    void DisplayUpgCondition(int level)
    {
        if (level < improvement.improvementMaxLevel)
        {
            ImprovementCost nextLevelUpgCondition = EventBus.SelectDevice.Value.GetImprovementsCosts().Find(cost => cost.upgradeLevel == level + 1);
            maximumLevelReachedText.gameObject.SetActive(false);
            foreach (Transform conditionObj in upgContainer)
            {
                if (conditionObj.GetSiblingIndex() < nextLevelUpgCondition.conditionsForUpgrade.Count)
                {
                    conditionObj.gameObject.SetActive(true);
                    SetConditionData(conditionObj, nextLevelUpgCondition);
                }
                else conditionObj.gameObject.SetActive(false);
            }
        }
        else
        {
            maximumLevelReachedText.gameObject.SetActive(true);
            upgContainer.gameObject.SetActive(false);
            upgEffectText.gameObject.SetActive(false);
            upgButton.gameObject.SetActive(false);
        }
    }

    void SetConditionData(Transform conditionObj, ImprovementCost condition)
    {
        int index = conditionObj.GetSiblingIndex();
        Sprite newResourceimage = resourceSprites.Find(sprite => sprite.name == condition.conditionsForUpgrade[index].resourceType.ToString());

        conditionObj.GetComponentInChildren<TextMeshProUGUI>().text = $"x {condition.conditionsForUpgrade[index].resAmount}";
        conditionObj.GetComponentInChildren<Image>().sprite = newResourceimage;
    }


    public void UpgradeLevel()
    {
        improvement.improvementLevel.Value++;
    }
}
       

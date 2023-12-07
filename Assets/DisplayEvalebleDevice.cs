using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayEvalebleDevice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI deviceName;
    [SerializeField] GameObject mainDescriptionWindow;
    [SerializeField] TextMeshProUGUI detailedDescriptionWindow;
    [SerializeField] GameObject deviceMatchText;
    [SerializeField] TextMeshProUGUI deviceNameInDescriptionWindow;
    [SerializeField] List<Sprite> resourceSprites;
    [SerializeField] List<GameObject> condidionsInDescriptionWindow;

    string tempDeviceName;
    IDevice inputDevice;
    List<Condition> createCondition = new();
    [SerializeField] List<InventoryItem> copyInventory = new();
    public void Activate<T>(T device)
    {
        GetDeviceData(device);
        SwitchDescriptionWindowActiveStatus(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SwitchDescriptionWindowActiveStatus(true);
        SetDescriptionData();
        FillDetailedDescriptionWindow();
        SetCreateCondotionData();
        CompareDevice(inputDevice);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SwitchDescriptionWindowActiveStatus(false);
    }

    void CompareDevice(IDevice device)
    {
        if (device.GetDeviceType() == DeviceType.Engine) { }
        else if (device.GetDeviceType() == DeviceType.RCS) { }
        else if (device.GetDeviceType() == DeviceType.Shield) { }
        else if (device.GetDeviceType() == DeviceType.Generator) { }
        else if (device.GetDeviceType() == DeviceType.TractorBeam && string.Compare(PlayerTractorBeam.currentTractorBeam.Value.GetDeviceID(), device.GetDeviceID()) == 0) { deviceMatchText.SetActive(true); }
        else if (device.GetDeviceType() == DeviceType.RepairDrone) { }
        else if (device.GetDeviceType() == DeviceType.Cargo && string.Compare(PlayerCargo.currentCargo.Value.GetDeviceID(), device.GetDeviceID()) == 0) { deviceMatchText.SetActive(true); }
        else deviceMatchText.SetActive(false);
    }

    void GetDeviceData<T>(T device)
    {
        inputDevice = device as IDevice;
        tempDeviceName = inputDevice.GetName();
        createCondition = inputDevice.GetCreateCondition();
        deviceName.text = tempDeviceName;
    }

    void SwitchDescriptionWindowActiveStatus(bool status)
    {
        mainDescriptionWindow.SetActive(status);
        detailedDescriptionWindow.transform.parent.gameObject.SetActive(status);
    }

    void SetDescriptionData()
    {
        deviceNameInDescriptionWindow.text = tempDeviceName;
        if (createCondition.Count == 2)
            condidionsInDescriptionWindow.ForEach(condition => condition.SetActive(true));
        else condidionsInDescriptionWindow[1].SetActive(false);
    }

    void SetCreateCondotionData()
    {
        for (int i = 0; i < createCondition.Count; i++)
        {
            Sprite newResourceimage = resourceSprites.Find(sprite => sprite.name == createCondition[i].resourceType.ToString());
            condidionsInDescriptionWindow[i].GetComponentInChildren<Image>().sprite = newResourceimage;

            condidionsInDescriptionWindow[i].GetComponentInChildren<TextMeshProUGUI>().text = $"x {createCondition[i].resAmount}";
        }
    }

    void FillDetailedDescriptionWindow()
    {
        detailedDescriptionWindow.text = "";
        List<Improvement> improvement = inputDevice.GetImprovements();

        for (int i = 0; i < improvement.Count; i++)
        {
            string newString = $"\r\n{improvement[i].improvementName}" +
            $"\r\nCur. {improvement[i].improvementLevel.Value * improvement[i].upgEffect} Max {improvement[i].improvementMaxLevel * improvement[i].upgEffect} ";
            detailedDescriptionWindow.text += newString;
        }
    }

    public void CreateAndSetDevice()
    {
        //copyInventory.Clear();
        //PlayerCargo.inventory.ForEach(item => copyInventory.Add(item));

        if (inputDevice.GetDeviceType() == DeviceType.Cargo)
        {
            //EventBus.CommandOnSortInventory?.Invoke(PlayerCargo.currentCargo.Value);
            if (CheckCompabilityNewCargoOnSlotsNumberAndCapacity(inputDevice as Cargo) == false) return;
        }

        if (EventBus.SpendResOnBuy?.Invoke(createCondition) == false)
        {            
            EventBus.CommandForPlaySound.Execute("errorSound");
            Debug.Log("Not enough Res");
            return;
        }

        if (inputDevice.GetDeviceType() == DeviceType.Engine) { }
        else if (inputDevice.GetDeviceType() == DeviceType.RCS) { }
        else if (inputDevice.GetDeviceType() == DeviceType.Shield) { }
        else if (inputDevice.GetDeviceType() == DeviceType.Generator) { }
        else if (inputDevice.GetDeviceType() == DeviceType.TractorBeam) { PlayerTractorBeam.currentTractorBeam.Value = inputDevice as TractorBeam; }
        else if (inputDevice.GetDeviceType() == DeviceType.RepairDrone) { }
        else if (inputDevice.GetDeviceType() == DeviceType.Cargo) { PlayerCargo.currentCargo.Value = inputDevice as Cargo;}

        EventBus.SelectDevice.Value = null;
        EventBus.CommandForPlaySound.Execute("successSound");
        SwitchDescriptionWindowActiveStatus(false);
    }

    bool CheckCompabilityNewCargoOnSlotsNumberAndCapacity(Cargo newCargo)
    {
        //EventBus.CommandOnSortInventory?.Invoke(newCargo);

        if (PlayerCargo.inventory.Count <= newCargo.CurrentSlotsNumber && PlayerCargo.inventory.TrueForAll(item => item.amount <= newCargo.CurrentSlotsCapacity))
        {
            //PlayerCargo.currentCargo.Value = newCargo;
            return true;
        }
        else
        {
            //PlayerCargo.inventory.Clear();
            //copyInventory.ForEach(item =>  PlayerCargo.inventory.Add(item));
            //EventBus.CommandOnRefreshUIInventory.Execute();
            EventBus.CommandForPlaySound.Execute("errorSound");
            Debug.Log("Not enough Space");
            return false;
        }
    }


}

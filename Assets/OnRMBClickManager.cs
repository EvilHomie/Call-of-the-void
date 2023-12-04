using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnRMBClickManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject selectDeviceMenu;
    [SerializeField] DeviceType slotType;

    RectTransform selectDeviceMenuRectTransform;
    RectTransform slotRectTransform;
    Vector2 offset = new (200, 0);
    SelectDeviceMenuManager selectDeviceMenuManager;

    private void Awake()
    {
        selectDeviceMenuRectTransform = selectDeviceMenu.GetComponent<RectTransform>();
        selectDeviceMenuManager = selectDeviceMenu.GetComponent<SelectDeviceMenuManager>();
        slotRectTransform = GetComponent<RectTransform>();
    }


    private void OnEnable()
    {
        SwitchselectDeviceMenuActiveStatus(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OpenSelectDeviceMenu();
    }

    void OpenSelectDeviceMenu()
    {
        switch (slotType)
        {
            case DeviceType.Engine:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesEngines);
                break;
            case DeviceType.RCS:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesRCSs);
                break;
            case DeviceType.Shield:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesShields);
                break;
            case DeviceType.Generator:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesGenerators);
                break;
            case DeviceType.TractorBeam:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesTractorBeams);
                break;
            case DeviceType.RepairDrone:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesRepairDrones);
                break;
            case DeviceType.Cargo:
                ActivateSelectDeviceMenu(EvaleblesDevice.evaleblesCargos);
                break;
        }
        SetDevicesMenuPosition();
    }

    void SetDevicesMenuPosition()
    {
        selectDeviceMenuRectTransform.anchoredPosition = slotRectTransform.anchoredPosition + offset;
    }  

    void ActivateSelectDeviceMenu<T>(List<T> deviceList)
    {
        SetDeviceListToSelectDeviceMenu(deviceList);
        SwitchselectDeviceMenuActiveStatus(true);
    }
    void SwitchselectDeviceMenuActiveStatus(bool activStatus)
    {
        selectDeviceMenu.SetActive(activStatus);
    }

    void SetDeviceListToSelectDeviceMenu<T>(List<T> deviceList)
    {
        selectDeviceMenuManager.Activate(deviceList);
    }
}

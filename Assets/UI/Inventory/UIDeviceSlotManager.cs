using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDeviceSlotManager : MonoBehaviour, IPointerClickHandler
{
    CompositeDisposable _disposable = new();

    [SerializeField] DeviceType slotType;
    [SerializeField] RawImage deviceImage;
    IDevice deviceInSlot;

    private void OnEnable()
    {
        ChoiseDeviceType();
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    void ChoiseDeviceType()
    {
        switch (slotType)
        {
            case DeviceType.Engine:
                break;
            case DeviceType.RCS:
                break;
            case DeviceType.Shield:
                break;
            case DeviceType.Generator:
                break;
            case DeviceType.TractorBeam:
                PlayerTractorBeam.currentTractorBeam.Subscribe(tBeam => ManageDeviceImage(tBeam)).AddTo(_disposable);
                break;
            case DeviceType.RepairDrone:
                break;
            case DeviceType.Cargo:
                PlayerCargo.currentCargo.Subscribe(cargo => ManageDeviceImage(cargo)).AddTo(_disposable);
                break;
        }
    }
    void ManageDeviceImage(IDevice device)
    {
        deviceInSlot = device;
        if (device != null)
        {
            deviceImage.texture = device.GetDeviceImage();
            deviceImage.gameObject.SetActive(true);
        }
        else deviceImage.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
            SelectDevice();        
    }

    void SelectDevice()
    {
        if (deviceInSlot != null)
            EventBus.SelectDevice.Value = deviceInSlot;
        else EventBus.CommandForShowError.Execute("No Device in Slot");
    }
}
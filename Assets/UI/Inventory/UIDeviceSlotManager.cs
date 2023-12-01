using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDeviceSlotManager : MonoBehaviour, IPointerClickHandler
{
    CompositeDisposable _disposable = new();

    [SerializeField] DeviceType slotType;
    [SerializeField] RawImage cargoImage;
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
                PlayerTractorBeam.currentTractorBeam.Subscribe(tBeam => ManageCargoImage(tBeam)).AddTo(_disposable);
                break;
            case DeviceType.RepairDrone:
                break;
            case DeviceType.Cargo:
                PlayerCargo.currentCargo.Subscribe(cargo => ManageCargoImage(cargo)).AddTo(_disposable);
                break;
        }
    }
    void ManageCargoImage(IDevice device)
    {
        deviceInSlot = device;
        if (device != null)
        {
            cargoImage.texture = device.GetDeviceImage();
            cargoImage.gameObject.SetActive(true);
        }
        else cargoImage.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (deviceInSlot != null)
            EventBus.SelectDevice.Value = deviceInSlot;
        else EventBus.CommandForShowError.Execute("No Device in Slot");
    }
}
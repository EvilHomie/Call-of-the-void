using UnityEngine;
using UnityEngine.EventSystems;

public class UICargoSlotManager : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        EventBus.SelectDevice.Value = PlayerCargo.currentCargo.Value;
    }
}
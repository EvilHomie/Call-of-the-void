using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerEnterManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject aditionResInSLotWindow;
    RectTransform aditionWindowRectTransform;
    AditionResWindowManager aditionResWindowManager;
    int slotIndex;

    private void Start()
    {
        aditionWindowRectTransform = aditionResInSLotWindow.GetComponent<RectTransform>();
        aditionResWindowManager = aditionResInSLotWindow.GetComponent<AditionResWindowManager>();
        slotIndex = transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        SwitchAditionWindowActivStatus(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerCargo.inventory.Count > slotIndex)
        {
            aditionResWindowManager.callSlotIndex = slotIndex;
            aditionWindowRectTransform.position = transform.position;
            SwitchAditionWindowActivStatus(true);
        }
    }

    void SwitchAditionWindowActivStatus(bool activStatus) 
    {
        aditionResInSLotWindow.SetActive(activStatus);
    }
}

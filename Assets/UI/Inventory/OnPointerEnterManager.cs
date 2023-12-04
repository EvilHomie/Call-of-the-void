using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerEnterManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject aditionResInSLotWindow;
    RectTransform aditionWindowRectTransform;
    AditionResWindowManager aditionResWindowManager;
    int slotIndex;

    private void Awake()
    {
        aditionWindowRectTransform = aditionResInSLotWindow.GetComponent<RectTransform>();
        aditionResWindowManager = aditionResInSLotWindow.GetComponent<AditionResWindowManager>();
        slotIndex = transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        SwitchAditionWindowActiveStatus(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerCargo.inventory.Count > slotIndex)
        {
            aditionResWindowManager.callSlotIndex = slotIndex;
            aditionWindowRectTransform.position = transform.position;
            SwitchAditionWindowActiveStatus(true);
        }
    }

    void SwitchAditionWindowActiveStatus(bool activStatus) 
    {
        aditionResInSLotWindow.SetActive(activStatus);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AditionResWindowManager : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI resName;
    [SerializeField] TextMeshProUGUI resAmount;

    public int callSlotIndex;
    InventoryItem matchItem;


    private void OnEnable()
    {
        matchItem = PlayerCargo.inventory[callSlotIndex];
        DisplayMatchRes();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    void DisplayMatchRes()
    {
        resName.text = matchItem.type.ToString();
        resAmount.text = "Amount " + matchItem.amount.ToString();
    }

    public void DropOneRes()
    {
        matchItem.amount--;        
        resAmount.text = "Amount " + matchItem.amount.ToString();
        EventBus.CommandOnRefreshUIInventory.Execute();

        if (matchItem.amount <= 0)
        {
            PlayerCargo.inventory.Remove(matchItem);
            gameObject.SetActive(false);
        }
        EventBus.CommandOnRefreshUIInventory.Execute();
    }
}

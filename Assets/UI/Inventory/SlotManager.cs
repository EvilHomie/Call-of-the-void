using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Image resInSlotImage;
    public TextMeshProUGUI resInSlotCount;
    public ResourceType resInSlotType;

    public void SetParameters(InventoryItem item)
    {
        resInSlotImage.sprite = item.image;
        resInSlotCount.text = item.count.ToString();
        resInSlotType = item.type;
    }
}

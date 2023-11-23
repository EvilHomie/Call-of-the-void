using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainInventorySlotManager : MonoBehaviour
{
    [SerializeField] Image resInSlotImage;
    [SerializeField] TextMeshProUGUI resInSlotCount;
    [SerializeField] ResourceType resInSlotType;

    public void SetParameters(InventoryItem item)
    {
        resInSlotImage.sprite = item.image;
        resInSlotCount.text = item.count.ToString();
        resInSlotType = item.type;
    }
}

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
        if (item != null)
        {
            SwitchDisplayMode(true);
            resInSlotImage.sprite = item.image;
            resInSlotCount.text = item.amount.ToString();
            resInSlotType = item.type;
        }
        else SwitchDisplayMode(false);        
    }

    void SwitchDisplayMode(bool enabled)
    {
        resInSlotImage.gameObject.SetActive(enabled);
        resInSlotCount.gameObject.SetActive(enabled);
    }
}

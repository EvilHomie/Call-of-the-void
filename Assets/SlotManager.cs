using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Image resInSlotImage;
    public TextMeshProUGUI resInSlotCount;
    public ResourceType resInSlotType;

    public void SetParameters(Sprite resInSlotImage, int count, ResourceType resInSlotType)
    {
        if (resInSlotImage != null)
            this.resInSlotImage.sprite = resInSlotImage;

        resInSlotCount.gameObject.SetActive(count > 0);
        resInSlotCount.text = count.ToString();
        this.resInSlotType = resInSlotType;

    }
}

using UniRx;
using UnityEngine;

public class UIDisplayInventory : MonoBehaviour
{
    CompositeDisposable _disposable = new();

    void OnEnable()
    {
        EventBus.InventoryActiveStatus.Subscribe(status => SwitchInventoryUI(status)).AddTo(_disposable);
    }

    void OnDisable()
    {
        _disposable.Clear();
    }

    void SwitchInventoryUI(bool status)
    {
        foreach (Transform inventoryElement in transform)
        {
            inventoryElement.gameObject.SetActive(status);
        }
    }
}



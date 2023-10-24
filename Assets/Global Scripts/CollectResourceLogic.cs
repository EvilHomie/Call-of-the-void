using UniRx;
using UnityEngine;

public class CollectResourceLogic : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip errorSound;

    void OnEnable()
    {
        EventBus.CommandOnCollectResource.Subscribe(res => PutInInventoryLogic(res)).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    void PutInInventoryLogic(GameObject resource)
    {
        ResourceItem curRes = resource.GetComponent<ResourceItem>();

        if (TryCombine(curRes)) return;

        if (PlayerDevices.inventory.Count < PlayerDevices.cargoCurentSlotsNumber)
        {
            InventoryItem newItem = new()
            {
                count = curRes.count,
                type = curRes.type,
                image = curRes.image
            };
            PlayerDevices.inventory.Add(newItem);
            EventBus.CommandForPlaySound.Execute(successSound);
            Destroy(resource);
        }
        else
        {
            EventBus.CommandForShowError.Execute("Not Enough Space In Cargo");
            EventBus.CommandForPlaySound.Execute(errorSound);
        }
    }

    bool TryCombine(ResourceItem resource)
    {
        InventoryItem compatibleItem = PlayerDevices.inventory.Find(item =>
        item.type == resource.type && item.count < PlayerDevices.cargoSlotCapacity);
        if (compatibleItem != null)
        {
            compatibleItem.count++;
            EventBus.CommandForPlaySound.Execute(successSound);
            Destroy(resource.gameObject);
            return true;
        }
        else return false;
    }
}

[System.Serializable]
public class InventoryItem
{
    public int count;
    public ResourceType type;
    public Sprite image;
}

using UniRx;
using UnityEngine;

public class CollectResourceLogic : MonoBehaviour
{
    CompositeDisposable _disposable = new();    

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

        if (PlayerCargo.inventory.Count < PlayerCargo.currentCargo.Value.CurrentSlotsNumber)
        {
            InventoryItem newItem = new()
            {
                amount = curRes.count,
                type = curRes.type,
                image = curRes.image
            };
            PlayerCargo.inventory.Add(newItem);
            EventBus.CommandForPlaySound.Execute("successSound");
            Destroy(resource);
        }
        else
        {
            EventBus.CommandForShowError.Execute("Not Enough Space In Cargo");
            EventBus.CommandForPlaySound.Execute("errorSound");
        }
    }

    bool TryCombine(ResourceItem resource)
    {
        InventoryItem compatibleItem = PlayerCargo.inventory.Find(item =>
        item.type == resource.type && item.amount < PlayerCargo.currentCargo.Value.CurrentSlotsCapacity);
        if (compatibleItem != null)
        {
            compatibleItem.amount++;
            EventBus.CommandForPlaySound.Execute("successSound");
            Destroy(resource.gameObject);
            return true;
        }
        else return false;
    }
}

[System.Serializable]
public class InventoryItem
{
    public int amount;
    public ResourceType type;
    public Sprite image;
}

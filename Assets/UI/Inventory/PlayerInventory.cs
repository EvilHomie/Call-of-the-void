using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] GameObject slotPrefab;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip errorSound;
    [SerializeField] GameObject slotsContainer;
    [SerializeField] List<InventoryItem> cargoSlotList = new();
    [SerializeField] int resCount;

    void OnEnable()
    {
        EventBus.CommandOnCollectResource.Subscribe(res => PutInInventoryLogic(res)).AddTo(_disposable);

        EventBus.InventoryActiveStatus.Subscribe(status => SwitchHud(status)).AddTo(_disposable);
    }
    void Start()
    {
        ÑreateSlots(GlobalData.CargoCurentSlotsNumber);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    void SwitchHud(bool status)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(status);
        }
        if (status)
            FillSlots();
    }

    void ÑreateSlots(int count)
    {
        if (cargoSlotList.Count < GlobalData.CargoMaxSlotNumber)
        {
            for (int i = 0; i < count; i++)
            {
                GlobalData.CargoCurentSlotsNumber++;
                InventoryItem item = new()
                {
                    resAmountInSlot = 0,
                    typeSlot = ResourceType.Nothing,
                    imageSlot = null
                };
                Instantiate(slotPrefab, slotsContainer.transform);
                cargoSlotList.Add(item);
            }
        }
        else
        {
            EventBus.CommandForShowError.Execute("Max Slots Count Reached");
            EventBus.CommandForPlaySound.Execute(errorSound);
        }
    }

    void FillSlots()
    {
        SlotManager[] slots = slotsContainer.GetComponentsInChildren<SlotManager>();

        for (int i = 0; i < cargoSlotList.Count; i++)
        {
            slots[i].SetParameters(cargoSlotList[i].imageSlot, cargoSlotList[i].resAmountInSlot, cargoSlotList[i].typeSlot);
        }

    }


    void PutInInventoryLogic(GameObject resource)
    {
        ResourceItem curRes = resource.GetComponent<ResourceItem>();

        InventoryItem compatibleSlot = cargoSlotList.Find(slot =>
        (slot.typeSlot == curRes.type || slot.typeSlot == ResourceType.Nothing) && slot.resAmountInSlot < GlobalData.CargoSlotCapacity);

        if (compatibleSlot != null)
        {
            compatibleSlot.resAmountInSlot++;
            compatibleSlot.typeSlot = curRes.type;
            compatibleSlot.imageSlot = curRes.image;
            EventBus.CommandForPlaySound.Execute(successSound);
            Destroy(resource);
        }
        else
        {
            EventBus.CommandForShowError.Execute("Not Enough Space In Cargo");
            EventBus.CommandForPlaySound.Execute(errorSound);
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public int resAmountInSlot;
    public ResourceType typeSlot;
    public Sprite imageSlot;
}


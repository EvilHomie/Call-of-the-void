using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIMainInventoryManager : MonoBehaviour
{
    CompositeDisposable _disposable_Cargo = new();
    CompositeDisposable _disposable_SlotLevel = new();
    [SerializeField] List<MainInventorySlotManager> mainInventorySlots = new();

    private void OnEnable()
    {
        PlayerCargo.currentCargo.Where(cargo => cargo != null).Subscribe(cargo =>
        {
            _disposable_SlotLevel.Clear();
            ActivateAndFillSlots(cargo);
        }).AddTo(_disposable_Cargo);
        RefreshDisplayResInSlots();
        EventBus.CommandOnRefreshUIInventory.Subscribe(_ =>RefreshDisplayResInSlots()).AddTo(_disposable_Cargo);
    }

    private void OnDisable()
    {
        _disposable_Cargo.Clear();
        _disposable_SlotLevel.Clear();
    }    



    void ActivateAndFillSlots(Cargo cargo)
    {
        cargo.slotsNumberLevel.Subscribe(level =>
        {
            ActivateEvalableSlots(level);
        }).AddTo(_disposable_SlotLevel);
    }



    void ActivateEvalableSlots(int amountActiveSlots)
    {
        foreach (MainInventorySlotManager slot in mainInventorySlots)
        {
            if (slot.transform.GetSiblingIndex() < amountActiveSlots)
            {
                slot.gameObject.SetActive(true);
            }
            else slot.gameObject.SetActive(false);
        }
    }
    void RefreshDisplayResInSlots()
    {
        foreach (MainInventorySlotManager slot in mainInventorySlots)
        {
            slot.SetParameters(null);
        }

        for (int i = 0; i < PlayerCargo.inventory.Count; i++)
        {
            mainInventorySlots[i].SetParameters(PlayerCargo.inventory[i]);
        }
    }
}

using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIMainInventoryManager : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] GameObject slotsContainer;
    [SerializeField] List<Transform> slots = new();
    

    void OnEnable()
    {
        PlayerCargo.InventoryActiveStatus.Subscribe(status => SwitchHud(status)).AddTo(_disposable);        
    }

    private void Start()
    {
        foreach (Transform slot in slotsContainer.transform)
        {
            slots.Add(slot);
        }
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
        {
            PlayerCargo.currentCargo.Value.slotsNumberLevel.Subscribe(_ => ActivateEvalableSlots()).AddTo(_disposable);
            SetInventoryResInSlots();
        }            
    }    

    void ActivateEvalableSlots()
    { 
        foreach (Transform slot in slots)
        {
            if (slot.GetSiblingIndex() < PlayerCargo.currentCargo.Value.CurrentSlotsNumber)
            {
                slot.gameObject.SetActive(true);
            }
            else slot.gameObject.SetActive(false);
        }
    }
    void SetInventoryResInSlots()
    {
        for (int i = 0; i < PlayerCargo.inventory.Count; i++)
        {
            slots[i].GetComponent<MainInventorySlotManager>().SetParameters(PlayerCargo.inventory[i]);
        }
    }
}



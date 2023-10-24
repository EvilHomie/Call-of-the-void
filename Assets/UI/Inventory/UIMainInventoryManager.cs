using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIMainInventoryManager : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] GameObject slotsContainer;
    List<Transform> slots = new();
    

    void OnEnable()
    {
        PlayerDevices.InventoryActiveStatus.Subscribe(status => SwitchHud(status)).AddTo(_disposable);
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
            ActivateEvalableSlots();
            SetResInSlots();
        }            
    }    

    void ActivateEvalableSlots()
    { 
        for (int i = 0; i < PlayerDevices.cargoCurentSlotsNumber; i++)
        {
            slots[i].gameObject.SetActive(true);
        }

        for (int i = PlayerDevices.cargoCurentSlotsNumber; i < PlayerDevices.cargoMaxSlotNumber; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
    void SetResInSlots()
    {
        for (int i = 0; i < PlayerDevices.inventory.Count; i++)
        {
            slots[i].GetComponent<SlotManager>().SetParameters(PlayerDevices.inventory[i]);
        }
    }
}



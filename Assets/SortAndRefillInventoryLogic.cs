using System.Collections.Generic;
using UnityEngine;

public class SortAndRefillInventoryLogic : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.CommandOnSortInventory += SortInventory;
    }

    private void OnDisable()
    {
        EventBus.CommandOnSortInventory -= SortInventory;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SortInventory(PlayerCargo.currentCargo.Value);
            EventBus.CommandOnRefreshUIInventory.Execute();
        }
    }
    void SortInventory(Cargo cargo)
    {
        //PlayerCargo playerCargo = FindAnyObjectByType<PlayerCargo>();
        //int cargoSlotCapasity = PlayerCargo.currentCargo.Value.CurrentSlotsCapacity;

        //for (int i = 0; i < playerCargo.testInventory.Count; i++) 
        //{
        //    InventoryItem compareItem = playerCargo.testInventory[i];
        //    InventoryItem matchItem = playerCargo.testInventory
        //        .Find(inventoryItem => inventoryItem.type == compareItem.type 
        //        && inventoryItem.amount < cargoSlotCapasity 
        //        && inventoryItem != compareItem);

        //    if (matchItem == null) continue;

        //    int freeSpace = cargoSlotCapasity - matchItem.amount;

        //    if (freeSpace >= compareItem.amount)
        //    {
        //        matchItem.amount += compareItem.amount;
        //        playerCargo.testInventory.Remove(compareItem);
        //    }
        //    else
        //    {                
        //        int mergeAmount = compareItem.amount - freeSpace;
        //        matchItem.amount += mergeAmount;
        //        compareItem.amount -= mergeAmount;
        //    }

        //}
        //EventBus.CommandOnRefreshUIInventory.Execute();

        //foreach (InventoryItem item in PlayerCargo.inventory) Find(inventoryItem => inventoryItem.type == CompareItem.type && inventoryItem.amount < cargoSlotCapasity && inventoryItem != CompareItem);
        //{
        //    InventoryItem matchItem = PlayerCargo.inventory.Find(inventoryItem => inventoryItem.type == item.type && inventoryItem.amount < cargoSlotCapasity);

        //    if (matchItem == null) continue;

        //    int freeSpace = cargoSlotCapasity - matchItem.amount;

        //    if (freeSpace >= item.amount)
        //    {
        //        matchItem.amount += item.amount;
        //        PlayerCargo.inventory.Remove(item);
        //    }
        //    else
        //    {
        //        int mergeAmount = item.amount - freeSpace;
        //        matchItem.amount += mergeAmount;
        //        item.amount -= mergeAmount;
        //    }
        //}

        Debug.Log("Sorting");
        // получаем общее число ресурсов каждого типа
        InventoryItem iron = CreateInvItem(ResourceType.Iron);
        InventoryItem copper = CreateInvItem(ResourceType.Copper);
        InventoryItem silver = CreateInvItem(ResourceType.Silver);
        InventoryItem gold = CreateInvItem(ResourceType.Gold);
        InventoryItem titanium = CreateInvItem(ResourceType.Titanium);

        List<InventoryItem> generalResList;
        List<InventoryItem> tempInventory;

        int cargoSlotCapacity = cargo.CurrentSlotsCapacity;

        foreach (InventoryItem inventoryItem in PlayerCargo.inventory)
        {
            if (inventoryItem.type == ResourceType.Iron) { iron.amount += inventoryItem.amount; }
            else if (inventoryItem.type == ResourceType.Copper) { copper.amount += inventoryItem.amount; }
            else if (inventoryItem.type == ResourceType.Silver) { silver.amount += inventoryItem.amount; }
            else if (inventoryItem.type == ResourceType.Gold) { gold.amount += inventoryItem.amount; }
            else if (inventoryItem.type == ResourceType.Titanium) { titanium.amount += inventoryItem.amount; }
        }
        // создаем лист с полученными ресурсами и новый инвентарь
        generalResList = new() { iron, copper, silver, gold, titanium };
        tempInventory = new();

        // перебор полученного листа с ресурсами и заполнение инвентаря 
        foreach (InventoryItem res in generalResList)
        {
            if (res.amount <= cargoSlotCapacity && res.amount != 0)
            {
                tempInventory.Add(res);
            }
            else if (res.amount > cargoSlotCapacity)
            {
                int tempCount = res.amount;

                while (tempCount > cargoSlotCapacity)
                {
                    InventoryItem tempItemFull = CreateInvItem(res.type);
                    tempItemFull.amount = cargoSlotCapacity;
                    tempInventory.Add(tempItemFull);
                    tempCount -= cargoSlotCapacity;
                }
                if (tempCount != 0)
                {
                    InventoryItem tempItem = CreateInvItem(res.type);
                    tempItem.amount = tempCount;
                    tempInventory.Add(tempItem);
                }
            }
        }
        PlayerCargo.inventory = new(tempInventory);
        EventBus.CommandOnRefreshUIInventory.Execute();
    }

    InventoryItem CreateInvItem(ResourceType resourceType)
    {
        InventoryItem inv = new()
        {
            type = resourceType,
            amount = 0,
            image = EventBus.CommandOnGetResImageByName?.Invoke(resourceType.ToString())
        };
        return inv;
    }
}

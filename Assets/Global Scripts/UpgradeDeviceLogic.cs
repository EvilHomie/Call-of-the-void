using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeDeviceLogic : MonoBehaviour
{
    //List<List<InventoryItem>> listOfsuitableResoucesList = new();


    private void OnEnable()
    {
        EventBus.OnUpgradeDevice += TryUpgradeDevice;
    }
    private void OnDisable()
    {
        EventBus.OnUpgradeDevice -= TryUpgradeDevice;
    }

    private bool TryUpgradeDevice(List<Condition> conditions)
    {
        if (CheckInventory(conditions))
        {

            return true;
        }
        else return false;
    }

    bool CheckInventory(List<Condition> conditions)
    {
        if (conditions.Any(condition => FindSuitableResources(condition) == false)) return false;

        for (int i = 0; i < conditions.Count; i++)
        {
            WasteResorces(conditions[i]);
        }

        return true;
    }

    bool FindSuitableResources(Condition condition)
    {
        // ���� ������� � ��������� �� ���� � ���������� false ���� �� ���
        List<InventoryItem> listSuitableForType = PlayerCargo.inventory.FindAll(res => res.type == condition.resourceType);
        if (listSuitableForType == null) return false;

        // ��������� ����� ���������� �� ���� �������� � ���������� false ���� �� ����� ������ ������������ �����
        List<int> suitableResCounts = listSuitableForType.Select(res => res.amount).ToList();
        int suitableResSumm = suitableResCounts.Sum();
        if(suitableResSumm < condition.resAmount) return false;

        // ���� ��� �������� �� ������ false, ��������� ������ ���������� �������� � ���������� true 
        //listOfsuitableResoucesList.Add(listSuitableForType);
        return true;
    }

    void WasteResorces(Condition condition)
    {
        int RequiredResAmount = condition.resAmount;

        while (RequiredResAmount > 0)
        {
            InventoryItem minAmountRes = PlayerCargo.inventory.Where(res => res.type == condition.resourceType).OrderBy(res => res.amount).First();
            if (RequiredResAmount > minAmountRes.amount)
            {
                RequiredResAmount -= minAmountRes.amount;
                PlayerCargo.inventory.Remove(minAmountRes);
                EventBus.CommandOnRefreshUIInventory.Execute();
            }
            else if (RequiredResAmount == minAmountRes.amount)
            {
                PlayerCargo.inventory.Remove(minAmountRes);
                EventBus.CommandOnRefreshUIInventory.Execute();
                return;
            }
            else if (RequiredResAmount < minAmountRes.amount)
            {
                minAmountRes.amount -= RequiredResAmount;
                EventBus.CommandOnRefreshUIInventory.Execute();
                return;
            }            
        }        
    }
}
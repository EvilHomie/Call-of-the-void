using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeDeviceLogic : MonoBehaviour
{
    //List<List<InventoryItem>> listOfsuitableResoucesList = new();


    private void OnEnable()
    {
        EventBus.SpendResOnBuy += TryUpgradeDevice;
    }
    private void OnDisable()
    {
        EventBus.SpendResOnBuy -= TryUpgradeDevice;
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
        // ищем ресурсы в инвентаре по типу и возвращаем false если их нет
        List<InventoryItem> listSuitableForType = PlayerCargo.inventory.FindAll(res => res.type == condition.resourceType);
        if (listSuitableForType == null) return false;

        // вычисляем сумму подходящих по типу ресурсов и возвращаем false если их сумма меньше необходимого числа
        List<int> suitableResCounts = listSuitableForType.Select(res => res.amount).ToList();
        int suitableResSumm = suitableResCounts.Sum();
        if(suitableResSumm < condition.resAmount) return false;

        // если все проверки не выдали false, заполняем список подходящих ресурсов и возвращаем true 
        //listOfsuitableResoucesList.Add(listSuitableForType);
        return true;
    }

    void WasteResorces(Condition condition)
    {
        int RequiredResAmount = condition.resAmount;

        while (RequiredResAmount > 0)
        {
            InventoryItem minAmountRes = PlayerCargo.inventory.Where(res => res.type == condition.resourceType).OrderBy(res => res.amount).Select(res => res).First();
            if (RequiredResAmount > minAmountRes.amount)
            {
                RequiredResAmount -= minAmountRes.amount;
                PlayerCargo.inventory.Remove(minAmountRes);                
            }
            else if (RequiredResAmount == minAmountRes.amount)
            {
                PlayerCargo.inventory.Remove(minAmountRes);                
                break;
            }
            else if (RequiredResAmount < minAmountRes.amount)
            {
                minAmountRes.amount -= RequiredResAmount;                
                break;
            }            
        }
        EventBus.CommandOnRefreshUIInventory.Execute();
    }
}

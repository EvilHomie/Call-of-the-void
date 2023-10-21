using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] List<InventoryItem> inventoryList = new();

    //[SerializeField] List<Test> test = new();

    [SerializeField] int resCount;

    private void OnEnable()
    {
        EventBus.ComandOnCollectResource.Subscribe(res => 
        {
            PutInInventory(res);
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        Test();
    //    }
    //}
    //void Test()
    //{
    //    test.Clear();
    //    foreach (InventoryItem item in inventoryList)
    //    {
    //        Test qwe = new()
    //        {
    //            type = item.type,
    //            amount = item.amount,
    //            image = item.image
    //        };
    //        test.Add(qwe);
    //    }
    //}


    void PutInInventory(GameObject resource)
    {
        ResourceItem curRes = resource.GetComponent<ResourceItem>();
        InventoryItem inventoryRes = new()
        {
            amount = curRes.resourceCount,
            type = curRes.type,
            image = curRes.image
        };
        inventoryList.Add(inventoryRes);
        Destroy(resource);
    }
}

[System.Serializable]
public class InventoryItem
{
    public int amount = 1;
    public ResourceType type;
    public Sprite image;
}


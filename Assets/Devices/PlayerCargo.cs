using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerCargo : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    public static ReactiveProperty<Cargo> currentCargo = new(null);
    public static List<InventoryItem> inventory = new();


    //[Header("TestingArea")]
    [SerializeField] Cargo testCargo_1;
    [SerializeField] Cargo testCargo_2;
    //[SerializeField] List<Improvement> TestImprovementsList = new();
    //[SerializeField] List<ImprovementCost> TestImprovementsCostList = new();


    //[SerializeField] List<InventoryItem> testInventory;

    private void Start()
    {
        TestSetNewCargo(testCargo_1);
    }
    void OnEnable()
    {
        currentCargo.Where(cargo => cargo != null).Subscribe(cargo =>
        {
            cargo.FillImprovementsList();
        }).AddTo(_disposable);
    }
    private void OnDestroy()
    {
        _disposable.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TestSetNewCargo(testCargo_2);
        }

        //    //if (inventory.Count > 0)
        //    //{
        //    //    Debug.Log("fwaf");
        //    //    testInventory = inventory;
        //    //}

    }

    void TestSetNewCargo(Cargo newCargo)
    {
        currentCargo.Value = Instantiate(newCargo);
        EventBus.SelectDevice.Value = null;
        //Test();
        //void Test()
        //{
        //    TestImprovementsList = currentCargo.Value.improvementsList;
        //    TestImprovementsCostList = currentCargo.Value.improvementsCostList;
        //    Debug.Log(currentCargo.Value.name);
        //}
    }
}

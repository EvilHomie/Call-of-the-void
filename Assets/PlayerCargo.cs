using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCargo : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    public static ReactiveProperty<Cargo> currentCargo = new(null);
    public static List<InventoryItem> inventory = new();
    public static BoolReactiveProperty InventoryActiveStatus = new(false);
    [SerializeField] RawImage UiCargoImage;


    [Header("TestingArea")]
    [SerializeField] Cargo testCargo;
    [SerializeField] List<Improvement> TestImprovementsList = new();
    [SerializeField] List<ImprovementCost> TestImprovementsCostList = new();
    void Start()
    {
        currentCargo.Subscribe(cargo =>
        {
            if (cargo != null)
            {
                cargo.FillImprovementsList();
                UiCargoImage.texture = cargo.deviceImage;
                UiCargoImage.gameObject.SetActive(true);
            }
            else UiCargoImage.gameObject.SetActive(false);

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
            TestSetNewCargo(testCargo);
        }
    }


    void TestSetNewCargo(Cargo newCargo)
    {
        currentCargo.Value = Instantiate(newCargo);
        Test();
        void Test()
        {
            TestImprovementsList = currentCargo.Value.improvementsList;
            TestImprovementsCostList = currentCargo.Value.improvementsCostList;
            Debug.Log(currentCargo.Value.name);
        }
    }
}

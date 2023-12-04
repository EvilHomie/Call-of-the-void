using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerTractorBeam : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    public static ReactiveProperty<TractorBeam> currentTractorBeam = new(null);


    [Header("TestingArea")]
    [SerializeField] TractorBeam testTBeam_1;
    [SerializeField] TractorBeam testTBeam_2;
    //[SerializeField] List<Improvement> TestImprovementsList = new();
    //[SerializeField] List<ImprovementCost> TestImprovementsCostList = new();


    private void Start()
    {
        TestSetNewTBeam(testTBeam_1);
    }
    //void OnEnable()
    //{
    //    currentTractorBeam.Where(tBeam => tBeam != null).Subscribe(tBeam =>
    //    {
    //        tBeam.FillImprovementsList();
    //    }).AddTo(_disposable);
    //}
    //private void OnDestroy()
    //{
    //    _disposable.Clear();
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TestSetNewTBeam(testTBeam_2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            TestSetNewTBeam(testTBeam_1);
        }
    }

    void TestSetNewTBeam(TractorBeam newTBeam)
    {
        currentTractorBeam.Value = Instantiate(newTBeam);
        EventBus.SelectDevice.Value = null;
        //Test();
        //void Test()
        //{
        //    TestImprovementsList = currentTractorBeam.Value.improvementsList;
        //    TestImprovementsCostList = currentTractorBeam.Value.improvementsCostList;
        //    Debug.Log(currentTractorBeam.Value.name);
        //}
    }
}

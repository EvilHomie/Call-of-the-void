using UniRx;
using UnityEngine;

public class EventBus: MonoBehaviour
{
    #region Events

    public static ReactiveCommand<GameObject> ComandOnCollectResource = new();
    public static BoolReactiveProperty TractorBeamActiveStatus = new(false); 
    
    public static ReactiveCommand<GameObject> ComandOnObjDie  = new();

    public static ReactiveCommand ComandOnTryGetTarget = new();
    public static ReactiveProperty<GameObject> SelectedTarget = new();

    public static ReactiveCommand ComandOnPlayerDie = new();
    public static ReactiveCommand ComandOnPlayerTakeDamage = new();    
    public static ReactiveCommand<GameObject> ComandOnSetPlayerParameters = new();

    public static FloatReactiveProperty AsteroidsSpawnMod = new(1);
    public static ReactiveCommand ComandOnAnomalyDestroy = new();
    public static ReactiveCommand<GameObject> ComandOnAnomalySpawn = new();
    public static ReactiveCommand ComandOnBigBGobjectDestroy = new();

    public static ReactiveCommand<GameObject> ComandOnStationSpawn = new();
    public static ReactiveCommand ComandOnStationDestroy = new();

    

    #endregion
    //private void OnEnable()
    //{
    //    ComandForCollectResource.Subscribe(res =>
    //    {
    //        Debug.Log(_disposable.Count);
    //    }).AddTo(_disposable);
    //}
}

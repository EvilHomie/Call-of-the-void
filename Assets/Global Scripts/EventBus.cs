using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EventBus: MonoBehaviour
{
    public static ReactiveCommand<GameObject> CommandOnCollectResource = new();    

    public static ReactiveCommand<GameObject> CommandOnObjDie  = new();

    public static ReactiveCommand CommandOnTryGetTarget = new();
    public static ReactiveProperty<GameObject> SelectedTarget = new();

    public static ReactiveCommand CommandOnPlayerDie = new();
    public static ReactiveCommand CommandOnPlayerTakeDamage = new();    
    public static ReactiveCommand<GameObject> CommandOnSetPlayerParameters = new();

    public static FloatReactiveProperty AsteroidsSpawnMod = new(1);
    public static ReactiveCommand CommandOnAnomalyDestroy = new();
    public static ReactiveCommand<GameObject> CommandOnAnomalySpawn = new();
    public static ReactiveCommand CommandOnBigBGobjectDestroy = new();

    public static ReactiveCommand<GameObject> CommandOnStationSpawn = new();
    public static ReactiveCommand CommandOnStationDestroy = new();


    public static ReactiveCommand<string> CommandForShowError = new();
    public static ReactiveCommand<string> CommandForPlaySound = new();

    public static BoolReactiveProperty InventoryActiveStatus = new(false);
    public static BoolReactiveProperty TractorBeamActiveStatus = new(false);



    public static ReactiveProperty<IDevice> SelectDevice = new();
    public static ReactiveCommand CommandOnCargoSlotIncrease = new();
    public static Func<List<Condition>, bool> SpendResOnBuy;
    public static ReactiveCommand CommandOnRefreshUIInventory = new();
    public static Action<Cargo> CommandOnSortInventory;
    public static Func<string, Sprite> CommandOnGetResImageByName;



    //private void OnEnable()
    //{
    //    ComandForCollectResource.Subscribe(res =>
    //    {
    //        Debug.Log(_disposable.Count);
    //    }).AddTo(_disposable);
    //}
}

using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerDevices : MonoBehaviour
{
    #region TractorBeam
    public static BoolReactiveProperty TractorBeamActiveStatus = new(false);    
    public static float tractorBeamGrabRadius = 25;
    public static float tractorBeamPullSpeed = 5;
    #endregion

    //#region Inventory
    //public static List<InventoryItem> inventory = new();
    //public static BoolReactiveProperty InventoryActiveStatus = new(false);
    //public static int cargoSlotCapacity = 10;
    //public static int cargoCurentSlotsNumber = 3;
    //public static int cargoMaxSlotNumber = 10;
    //#endregion



}


using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static Transform PlayerTransform;
    public static Vector3 PlayerVelocity;
    public static Vector3 MousePos;



    public static float GrabRadius = 25;
    public static float PullSpeed = 5;

    public static int CargoSlotCapacity = 10;
    public static int CargoCurentSlotsNumber = 3;
    public static int CargoMaxSlotNumber = 10;

    public static float EnemyStrengthMultiplier = 1;

}
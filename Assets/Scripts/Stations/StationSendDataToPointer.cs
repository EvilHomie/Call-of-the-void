using System;
using UnityEngine;

public class StationSendDataToPointer : MonoBehaviour
{
    public static Action<Vector3, string> sendStationData;

    new string[] name;

    private void OnEnable()
    {
        name = gameObject.name.Split(' ');
        sendStationData?.Invoke(transform.position, name[0]);
    }
    private void OnDestroy()
    {
        sendStationData?.Invoke(Vector3.zero, name[0]);
    }
}

using System;
using UnityEngine;

public class StationSendDataToPointer : MonoBehaviour
{
    new string[] name;

    private void OnEnable()
    {
        name = gameObject.name.Split(' ');
        EventBus.sendStationData?.Invoke(transform.position, name[0]);
    }
    private void OnDestroy()
    {
        EventBus.sendStationData?.Invoke(Vector3.zero, name[0]);
    }
}

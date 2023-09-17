using System;
using UnityEngine;

public class BroadcastStationPosition : MonoBehaviour
{
    public static Action<Vector3> broadcastStationPosition;
    private void Awake()
    {
        broadcastStationPosition?.Invoke(transform.position);
    }
    private void OnDestroy()
    {
        broadcastStationPosition?.Invoke(Vector3.zero);
    }
}

using System;
using UnityEngine;

public class BroadcastAnomalyZonePosition : MonoBehaviour
{
    public static Action<Vector3> broadcastAnomalyZonePosition;

    Vector3 offset = new(0, 5, 0);
    private void OnEnable()
    {
        broadcastAnomalyZonePosition?.Invoke(transform.position - offset);
        Debug.Log(transform.position);
    }
    private void OnDestroy()
    {
        broadcastAnomalyZonePosition?.Invoke(Vector3.zero);
    }
}

using System;
using UnityEngine;

public class AnomalyZoneManager : MonoBehaviour
{
    public static Action onAnomalyZoneDestroy;

    private void OnDisable()
    {
        onAnomalyZoneDestroy?.Invoke();
    }
}

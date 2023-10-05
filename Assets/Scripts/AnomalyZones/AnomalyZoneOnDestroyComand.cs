using System;
using UnityEngine;

public class AnomalyZoneOnDestroyComand : MonoBehaviour
{
    public static Action onAnomalyZoneDestroy;

    private void OnDestroy()
    {
        onAnomalyZoneDestroy?.Invoke();
    }
}

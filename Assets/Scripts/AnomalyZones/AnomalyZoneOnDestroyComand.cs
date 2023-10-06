using System;
using UnityEngine;

public class AnomalyZoneOnDestroyComand : MonoBehaviour
{   
    private void OnDestroy()
    {
        EventBus.onAnomalyZoneDestroy?.Invoke();
    }
}

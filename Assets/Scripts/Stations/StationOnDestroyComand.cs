using System;
using UnityEngine;

public class StationOnDestroyComand : MonoBehaviour
{
    public static Action onStationDestroy;

    private void OnDestroy()
    {
        onStationDestroy?.Invoke();
    }
}

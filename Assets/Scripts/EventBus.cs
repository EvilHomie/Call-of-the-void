using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus: MonoBehaviour
{
    public static Action<Vector3> broadcastPlayerVelocity;
    public static Action<Vector3> broadcastMousePosition;
    public static Action<Transform> broadcastPlayerTransform;

    public static Action<Vector3, string> sendStationData;
    public static Action onAnomalyZoneDestroy;
    public static Action<Vector3, string> sendAnomalyData;

    public static Action shakeCam;

    public static Action<string, float, float, float> playerMaxParameters;
    public static Action<float, float, float> playerCurrentParameters;
    public static Action<string, float, float, float> targetMaxParameters;
    public static Action<float, float, float> targetCurrentParameters;

    public static Action<List<Resource>, Vector3> spawnResources;
}

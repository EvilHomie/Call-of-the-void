using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus: MonoBehaviour
{
    public static Action<Vector3, string> onStationSpawn;
    public static Action onStationDestroy;
    public static Action onBGBigObjectDestroy;

    public static Action<Vector3, string> onAnomalySpawn;
    public static Action onAnomalyDestroy;
    public static Action<float> onPlayerInAsteroidField;

    

    public static Action<GameObject> onSetPlayerParameters;
    public static Action onPlayerTakeDamage;
    public static Action onPlayerDie;

    public static Action<GameObject> onSelectTarget;
    public static Action onDeselectTarget;



    public static Action<GameObject> onObjDie;
    public static Action<List<Resource>, Vector3> spawnResources;

    public static Action<GameObject> onCollectResource;
}

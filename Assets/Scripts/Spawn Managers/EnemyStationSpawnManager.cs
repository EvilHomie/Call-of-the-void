using UnityEngine;

public class EnemyStationSpawnManager : SpawnManager
{
    protected override void OnEnable()
    {         
        base.OnEnable();
        BroadcastStationPosition.broadcastStationPosition += Invoke;
        SetDelayAndSpawn();
    }

    protected override void OnDisable()
    {        
        base.OnDisable();
        BroadcastStationPosition.broadcastStationPosition -= Invoke;
    }   

    void Invoke(Vector3 stationPos)
    {
        if (stationPos == Vector3.zero)
        {
            SetDelayAndSpawn();
        }
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

using UnityEngine;

public class AnomalyZoneSpawnManager : SpawnManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        AnomalyZoneManager.onAnomalyZoneDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        AnomalyZoneManager.onAnomalyZoneDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

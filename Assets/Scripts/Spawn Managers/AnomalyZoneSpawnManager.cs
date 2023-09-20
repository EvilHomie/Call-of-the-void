using UnityEngine;

public class AnomalyZoneSpawnManager : SpawnManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        AnomalyZoneOnDestroyComand.onAnomalyZoneDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        AnomalyZoneOnDestroyComand.onAnomalyZoneDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

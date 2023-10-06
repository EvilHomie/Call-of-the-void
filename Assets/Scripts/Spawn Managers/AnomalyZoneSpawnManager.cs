using UnityEngine;

public class AnomalyZoneSpawnManager : SpawnManagerLogic
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.onAnomalyZoneDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.onAnomalyZoneDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

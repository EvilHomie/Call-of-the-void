using UnityEngine;

public class AnomalyZoneSpawnManager : SpawnManagerLogic
{
    void OnEnable()
    {
        EventBus.onAnomalyDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    void OnDisable()
    {
        EventBus.onAnomalyDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

using UnityEngine;

public class EnemyStationSpawnManager : SpawnManagerLogic
{
    void OnEnable()
    {         
        EventBus.onStationDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }

    void OnDisable()
    {
        EventBus.onStationDestroy -= SetDelayAndSpawn;
    }   

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

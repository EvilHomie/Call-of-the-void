using UnityEngine;

public class BigObjectSpawnManager : SpawnManagerLogic
{
    void OnEnable()
    {
        EventBus.onBGBigObjectDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    void OnDisable()
    {
        EventBus.onBGBigObjectDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

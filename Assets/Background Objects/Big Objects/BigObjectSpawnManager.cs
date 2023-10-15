using UnityEngine;

public class BigObjectSpawnManager : SpawnManagerLogic
{
    void OnEnable()
    {
        BigObjectManager.onBigObjectDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    void OnDisable()
    {
        BigObjectManager.onBigObjectDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

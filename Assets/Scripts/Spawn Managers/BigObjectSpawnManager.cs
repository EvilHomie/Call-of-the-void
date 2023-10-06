using UnityEngine;

public class BigObjectSpawnManager : SpawnManagerLogic
{
    protected override void OnEnable()
    {
        base.OnEnable();
        BigObjectManager.onBigObjectDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        BigObjectManager.onBigObjectDestroy -= SetDelayAndSpawn;
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

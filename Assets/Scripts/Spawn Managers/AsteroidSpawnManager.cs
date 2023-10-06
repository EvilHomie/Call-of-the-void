using UnityEngine;

public class AsteroidSpawnManager : SpawnManagerLogic
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        RepitSpawn();
        PlayerInFiledManager.comandToAsteroids += ChangeSpawnDelay;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerInFiledManager.comandToAsteroids -= ChangeSpawnDelay;
    }

    void RepitSpawn()
    {
        ChoiseSpawnMethod();
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }

    void ChangeSpawnDelay(float multipler)
    {
        minSpawnDelay /= multipler;
        maxSpawnDelay /= multipler;
    }
}

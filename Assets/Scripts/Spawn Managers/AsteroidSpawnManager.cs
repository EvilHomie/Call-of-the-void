using UnityEngine;

public class AsteroidSpawnManager : SpawnManager
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        RepitSpawn();
        AsteriodFieldManager.comandToAsteroids += ChangeSpawnDelay;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        AsteriodFieldManager.comandToAsteroids -= ChangeSpawnDelay;
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

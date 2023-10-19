using UnityEngine;

public class AsteroidSpawnManager : SpawnManagerLogic
{
    float defMinSpawnDelay;
    float defMaxSpawnDelay;
    void OnEnable()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
        defMinSpawnDelay = minSpawnDelay;
        defMaxSpawnDelay = maxSpawnDelay;
        EventBus.onPlayerInAsteroidField += ChangeSpawnDelay;
    }

    void OnDisable()
    {
        EventBus.onPlayerInAsteroidField -= ChangeSpawnDelay;
    }

    void RepitSpawn()
    {
        ChoiseSpawnMethod();
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }

    void ChangeSpawnDelay(float multipler)
    {
        minSpawnDelay = defMinSpawnDelay / multipler;
        maxSpawnDelay = defMaxSpawnDelay / multipler;
    }
}

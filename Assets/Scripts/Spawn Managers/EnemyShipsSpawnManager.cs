using UnityEngine;

public class EnemyShipsSpawnManager : SpawnManagerLogic
{
    void OnEnable()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void RepitSpawn()
    {
        ChoiseSpawnMethod();
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }   
}

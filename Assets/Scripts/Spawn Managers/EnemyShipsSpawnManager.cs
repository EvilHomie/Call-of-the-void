using UnityEngine;

public class EnemyShipsSpawnManager : SpawnManagerLogic
{
    void OnEnable()
    {        
        RepitSpawn();        
    }

    void RepitSpawn()
    {
        ChoiseSpawnMethod();
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }   
}

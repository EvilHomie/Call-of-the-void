using UnityEngine;

public class EnemyShipsSpawnManager : SpawnManagerLogic
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        RepitSpawn();        
    }

    protected override void OnDisable()
    {
        base.OnDisable();        
    }

    void RepitSpawn()
    {
        ChoiseSpawnMethod();
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }   
}

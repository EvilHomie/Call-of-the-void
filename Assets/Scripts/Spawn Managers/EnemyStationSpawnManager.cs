using UnityEngine;

public class EnemyStationSpawnManager : SpawnManagerLogic
{
    protected override void OnEnable()
    {         
        base.OnEnable();
        StationOnDestroyComand.onStationDestroy += SetDelayAndSpawn;
        SetDelayAndSpawn();
    }

    protected override void OnDisable()
    {        
        base.OnDisable();
        StationOnDestroyComand.onStationDestroy -= SetDelayAndSpawn;
    }   

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

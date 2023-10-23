using UniRx;
using UnityEngine;

public class AsteroidSpawnManager : SpawnManagerLogic
{
    CompositeDisposable _disposables = new();
    float defMinSpawnDelay;
    float defMaxSpawnDelay;
    void OnEnable()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
        defMinSpawnDelay = minSpawnDelay;
        defMaxSpawnDelay = maxSpawnDelay;
        EventBus.AsteroidsSpawnMod.Subscribe(mod=> ChangeSpawnDelay(mod)).AddTo(_disposables);
    }

    void OnDisable()
    {
        _disposables.Clear();
        CancelInvoke();
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

using UniRx;
using UnityEngine;

public class BigObjectSpawnManager : SpawnManagerLogic
{
    CompositeDisposable _disposables = new();
    void OnEnable()
    {
        EventBus.ComandOnBigBGobjectDestroy.Subscribe(_ => SetDelayAndSpawn()).AddTo(_disposables);
        SetDelayAndSpawn();
    }
    void OnDisable()
    {
        _disposables.Clear();
    }

    void SetDelayAndSpawn()
    {
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(ChoiseSpawnMethod), spawnDelay);
    }
}

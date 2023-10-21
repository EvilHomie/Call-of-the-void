using UniRx;

public class StationPointerManager : PointerLogic
{
    CompositeDisposable _disposables = new();
    void OnEnable()
    {
        EventBus.ComandOnStationSpawn.Subscribe(station => SetTargetData(station)).AddTo(_disposables);
        EventBus.ComandOnStationDestroy.Subscribe(_ => DisablePointer()).AddTo(_disposables);
    }

    void OnDisable()
    {
        _disposables.Clear();
    }
}

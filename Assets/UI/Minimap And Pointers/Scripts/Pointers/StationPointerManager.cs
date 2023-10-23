using UniRx;

public class StationPointerManager : PointerLogic
{
    CompositeDisposable _disposables = new();
    void OnEnable()
    {
        EventBus.CommandOnStationSpawn.Subscribe(station => SetTargetData(station)).AddTo(_disposables);
        EventBus.CommandOnStationDestroy.Subscribe(_ => DisablePointer()).AddTo(_disposables);
    }

    void OnDisable()
    {
        _disposables.Clear();
    }
}

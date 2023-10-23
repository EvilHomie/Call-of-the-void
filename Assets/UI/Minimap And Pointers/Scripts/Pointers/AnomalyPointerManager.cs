using UniRx;

public class AnomalyPointerManager : PointerLogic
{
    CompositeDisposable _disposables = new();
    void OnEnable()
    {
        EventBus.CommandOnAnomalySpawn.Subscribe(anomaly => SetTargetData(anomaly)).AddTo(_disposables);
        EventBus.CommandOnAnomalyDestroy.Subscribe(_ => DisablePointer()).AddTo(_disposables);
    }

    void OnDisable()
    {
        _disposables.Clear();
    }
}

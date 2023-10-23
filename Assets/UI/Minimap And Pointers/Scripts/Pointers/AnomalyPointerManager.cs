using UniRx;

public class AnomalyPointerManager : PointerLogic
{
    CompositeDisposable _disposables = new();
    void OnEnable()
    {
        EventBus.ComandOnAnomalySpawn.Subscribe(anomaly => SetTargetData(anomaly)).AddTo(_disposables);
        EventBus.ComandOnAnomalyDestroy.Subscribe(_ => DisablePointer()).AddTo(_disposables);
    }

    void OnDisable()
    {
        _disposables.Clear();
    }
}

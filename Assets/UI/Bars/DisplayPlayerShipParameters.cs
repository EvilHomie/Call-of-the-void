using UniRx;

public class DisplayPlayerShipParameters : DisplayParametersLogic
{
    CompositeDisposable _disposables = new();
    protected override void OnEnable()
    {
        EventBus.CommandOnSetPlayerParameters
            .Where(player => player != null)
            .Subscribe(player => { EnableTargetDisplay(player); } ).AddTo( _disposables );

    }
    protected override void OnDisable()
    {
        _disposables.Clear();
    }
}

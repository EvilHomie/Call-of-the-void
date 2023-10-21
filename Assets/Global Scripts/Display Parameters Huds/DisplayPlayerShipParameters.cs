using UniRx;

public class DisplayPlayerShipParameters : DisplayParametersLogic
{
    CompositeDisposable _disposables = new();
    protected override void OnEnable()
    {
        EventBus.ComandOnSetPlayerParameters
            .Where(player => player != null)
            .Subscribe(player => { EnableTargetDisplay(player); } ).AddTo( _disposables );

    }
    protected override void OnDisable()
    {
        _disposables.Clear();
    }
}

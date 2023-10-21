using UniRx;

public class DisplayTargetParameters : DisplayParametersLogic
{
    CompositeDisposable _disposable = new();

    protected override void OnEnable()
    {
        EventBus.SelectedTarget
            .Subscribe(target =>
            {
                if (target != null)
                    EnableTargetDisplay(target);
                else
                    DesableTargetDisplay();
            }).AddTo(_disposable);
    }
    protected override void OnDisable()
    {

        _disposable.Clear();
    }
}

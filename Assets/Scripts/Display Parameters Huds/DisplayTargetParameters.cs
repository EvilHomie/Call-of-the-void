public class DisplayTargetParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        EventBus.onSelectTarget += SetMaxParameters;
        EventBus.onDeselectTarget += TurnOffDisplay;
        SwitchBars(false);
    }
    protected override void OnDisable()
    {
        EventBus.onSelectTarget -= SetMaxParameters;
        EventBus.onDeselectTarget -= TurnOffDisplay;
    }
}

public class DisplayPlayerShipParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        EventBus.onSetPlayerParameters += SetMaxParameters;
        EventBus.onPlayerDie += TurnOffDisplay;
    }
    protected override void OnDisable()
    {
        EventBus.onSetPlayerParameters -= SetMaxParameters;
        EventBus.onPlayerDie -= TurnOffDisplay;
    }
}

public class DisplayPlayerShipParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        EventBus.playerMaxParameters += SetMaxParameters;
        EventBus.playerCurrentParameters += DisplayCurrentParameters;
    }
    protected override void OnDisable()
    {
        EventBus.playerMaxParameters -= SetMaxParameters;
        EventBus.playerCurrentParameters -= DisplayCurrentParameters;
    }
}

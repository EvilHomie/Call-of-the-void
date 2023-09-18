public class DisplayPlayerShipParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        BroadcastPlayerParameters.playerMaxParameters += SetMaxParameters;
        BroadcastPlayerParameters.playerCurrentParameters += DisplayCurrentParameters;
    }
    protected override void OnDisable()
    {
        BroadcastPlayerParameters.playerMaxParameters -= SetMaxParameters;
        BroadcastPlayerParameters.playerCurrentParameters -= DisplayCurrentParameters;
    }
}

public class DisplayPlayerShipParameters : DisplayParameters
{
    public override void OnEnable()
    {
        BroadcastPlayerParameters.playerMaxParameters += SetMaxParameters;
        BroadcastPlayerParameters.playerCurrentParameters += DisplayCurrentParameters;
    }
    public override void OnDisable()
    {
        BroadcastPlayerParameters.playerMaxParameters -= SetMaxParameters;
        BroadcastPlayerParameters.playerCurrentParameters -= DisplayCurrentParameters;
    }
}

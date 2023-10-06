public class DisplayTargetParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        EventBus.targetMaxParameters += SetMaxParameters;
        EventBus.targetCurrentParameters += DisplayCurrentParameters;
    }
    protected override void OnDisable()
    {
        EventBus.targetMaxParameters -= SetMaxParameters;
        EventBus.targetCurrentParameters -= DisplayCurrentParameters;
    }
}

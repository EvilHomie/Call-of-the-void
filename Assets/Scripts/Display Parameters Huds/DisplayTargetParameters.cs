public class DisplayTargetParameters : DisplayParameters
{
    protected override void OnEnable()
    {
        TargetManager.targetMaxParameters += SetMaxParameters;
        TargetManager.targetCurrentParameters += DisplayCurrentParameters;
    }
    protected override void OnDisable()
    {
        TargetManager.targetMaxParameters -= SetMaxParameters;
        TargetManager.targetCurrentParameters -= DisplayCurrentParameters;
    }
}

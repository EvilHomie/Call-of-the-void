public class DisplayTargetParameters : DisplayParameters
{
    public override void OnEnable()
    {
        TargetManager.targetMaxParameters += SetMaxParameters;
        TargetManager.targetCurrentParameters += DisplayCurrentParameters;
    }
    public override void OnDisable()
    {
        TargetManager.targetMaxParameters -= SetMaxParameters;
        TargetManager.targetCurrentParameters -= DisplayCurrentParameters;
    }
}

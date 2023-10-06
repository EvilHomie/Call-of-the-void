public class AnomalyPointerManager : PointerManager
{  
    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.sendAnomalyData += GetTargetData;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.sendAnomalyData -= GetTargetData;
    }
}

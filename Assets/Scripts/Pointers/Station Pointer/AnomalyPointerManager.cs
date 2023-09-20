public class AnomalyPointerManager : PointerManager
{  
    protected override void OnEnable()
    {
        base.OnEnable();
        AnomalyZoneSendDataToPointer.sendAnomalyData += GetTargetData;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        AnomalyZoneSendDataToPointer.sendAnomalyData -= GetTargetData;
    }
}

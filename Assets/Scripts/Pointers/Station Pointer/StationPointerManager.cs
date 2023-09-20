public class StationPointerManager : PointerManager
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        StationSendDataToPointer.sendStationData += GetTargetData;
    }

    protected override void OnDisable()
    {      
        base.OnDisable();
        StationSendDataToPointer.sendStationData -= GetTargetData;
    }
}

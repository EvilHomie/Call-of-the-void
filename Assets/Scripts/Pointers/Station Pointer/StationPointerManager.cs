public class StationPointerManager : PointerManager
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        EventBus.sendStationData += GetTargetData;
    }

    protected override void OnDisable()
    {      
        base.OnDisable();
        EventBus.sendStationData -= GetTargetData;
    }
}

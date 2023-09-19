public class StationPointerManager : PointerManager
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        BroadcastStationPosition.broadcastStationPosition += GetTargetPos;
    }

    protected override void OnDisable()
    {      
        base.OnDisable();
        BroadcastStationPosition.broadcastStationPosition -= GetTargetPos;
    }
}

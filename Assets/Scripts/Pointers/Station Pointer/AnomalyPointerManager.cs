public class AnomalyPointerManager : PointerManager
{
    protected override void OnEnable()
    {
        base.OnEnable();
        BroadcastAnomalyZonePosition.broadcastAnomalyZonePosition += GetTargetPos;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        BroadcastAnomalyZonePosition.broadcastAnomalyZonePosition -= GetTargetPos;
    }
}

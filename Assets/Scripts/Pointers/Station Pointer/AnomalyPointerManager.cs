public class AnomalyPointerManager : PointerLogic
{  
    void OnEnable()
    {
        EventBus.onAnomalySpawn += GetTargetData;
        EventBus.onAnomalyDestroy += DisablePointer;
    }

    void OnDisable()
    {
        EventBus.onAnomalySpawn -= GetTargetData;
        EventBus.onAnomalyDestroy -= DisablePointer;
    }
}

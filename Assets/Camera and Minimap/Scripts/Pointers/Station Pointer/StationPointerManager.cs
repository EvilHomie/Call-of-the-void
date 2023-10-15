public class StationPointerManager : PointerLogic
{
    void OnEnable()
    {        
        EventBus.onStationSpawn += GetTargetData;
        EventBus.onStationDestroy += DisablePointer;
    }

    void OnDisable()
    {      
        EventBus.onStationSpawn -= GetTargetData;
        EventBus.onStationDestroy -= DisablePointer;
    }
}

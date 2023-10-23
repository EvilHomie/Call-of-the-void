using UnityEngine;

public class StationManager : MonoBehaviour
{
    private void OnEnable()
    {        
        EventBus.CommandOnStationSpawn.Execute(gameObject);
    }
    private void OnDestroy()
    {
        EventBus.CommandOnStationDestroy.Execute();
    }
}

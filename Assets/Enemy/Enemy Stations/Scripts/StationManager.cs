using UnityEngine;

public class StationManager : MonoBehaviour
{
    private void OnEnable()
    {        
        EventBus.ComandOnStationSpawn.Execute(gameObject);
    }
    private void OnDestroy()
    {
        EventBus.ComandOnStationDestroy.Execute();
    }
}

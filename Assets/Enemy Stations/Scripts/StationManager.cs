using UnityEngine;

public class StationManager : MonoBehaviour
{
    new string[] name;

    private void OnEnable()
    {
        name = gameObject.name.Split(' ');
        EventBus.onStationSpawn?.Invoke(transform.position, name[0]);
    }
    private void OnDestroy()
    {
        EventBus.onStationDestroy?.Invoke();
    }
}

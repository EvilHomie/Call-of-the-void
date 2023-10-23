using UnityEngine;

public class AnomalyZoneManager : MonoBehaviour
{   
    private void OnEnable()
    {
        EventBus.CommandOnAnomalySpawn.Execute(gameObject);
    }
    private void OnDestroy()
    {
        EventBus.CommandOnAnomalyDestroy.Execute();
    }
}

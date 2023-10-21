using UnityEngine;

public class AnomalyZoneManager : MonoBehaviour
{   
    private void OnEnable()
    {
        EventBus.ComandOnAnomalySpawn.Execute(gameObject);
    }
    private void OnDestroy()
    {
        EventBus.ComandOnAnomalyDestroy.Execute();
    }
}

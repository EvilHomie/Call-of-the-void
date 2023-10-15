using UnityEngine;

public class AnomalyZoneManager : MonoBehaviour
{   
    new string[] name;
    Vector3 offset;
    private void OnEnable()
    {
        offset = new Vector3(0, transform.position.y, 0);
        name = gameObject.name.Split ('(');
        EventBus.onAnomalySpawn?.Invoke(transform.position - offset, name[0]);
    }
    private void OnDestroy()
    {
        EventBus.onAnomalyDestroy?.Invoke();
    }
}

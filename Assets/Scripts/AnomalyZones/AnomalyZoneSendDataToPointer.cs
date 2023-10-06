using System;
using UnityEngine;

public class AnomalyZoneSendDataToPointer : MonoBehaviour
{   
    Vector3 offset;
    new string[] name;
    private void OnEnable()
    {
        offset = new Vector3 (0, transform.position.y, 0);
        name = gameObject.name.Split ('(');
        EventBus.sendAnomalyData?.Invoke(transform.position - offset, name[0]);
    }
    private void OnDestroy()
    {
        EventBus.sendAnomalyData?.Invoke(Vector3.zero, name[0]);
    }
}

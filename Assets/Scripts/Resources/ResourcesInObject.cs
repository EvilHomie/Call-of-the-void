using System.Collections.Generic;
using UnityEngine;

public class ResourcesInObject: MonoBehaviour
{
    [SerializeField] List<Resource> resourcesInObj;

    //private void OnEnable()
    //{
    //    PlayerControl.broadcastPlayerTransform += GetPlayerPos;
    //    InvokeRepeating(nameof(CheckDistance), delayCheckDistance, delayCheckDistance);
    //}
    //private void OnDisable()
    //{
    //    PlayerControl.broadcastPlayerTransform -= GetPlayerPos;
    //}
    public void OnDestroy()
    {
        //if()
        EventBus.spawnResources?.Invoke(resourcesInObj, transform.position);
    }
}

[System.Serializable]
public class Resource
{
    public ResourceType type;
    public int dropChance;
    public int amount;
}

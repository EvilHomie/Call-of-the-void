using System.Collections.Generic;
using UnityEngine;

public class ResourcesInObject: MonoBehaviour
{
    [SerializeField] List<Resource> resourcesInObj;

    private void OnDestroy()
    {
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

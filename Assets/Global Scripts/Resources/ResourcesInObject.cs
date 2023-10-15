using System.Collections.Generic;
using UnityEngine;

public class ResourcesInObject: MonoBehaviour
{
    public List<Resource> resourcesInObj;    
}

[System.Serializable]
public class Resource
{
    public ResourceType type;
    public int dropChance;
    public int amount;
}

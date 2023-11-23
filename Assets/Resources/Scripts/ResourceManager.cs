public class ResourceManager
{
    
}
public enum ResourceType
{
    Iron,
    Copper,
    Silver,
    Gold,
    Titanium
}

[System.Serializable]
public class Resource
{
    public ResourceType type;
    public int dropChance;
    public int amount;
}

using System.Collections.Generic;
using UnityEngine;

public class SpawnResourcesLogic : MonoBehaviour
{
    [SerializeField] List<Sprite> resourceSprites;
    [SerializeField] GameObject resourceItemPrefab;

    private void OnEnable()
    {
        EventBus.spawnResources += SpawnreSources;
    }

    private void OnDisable()
    {
        EventBus.spawnResources -= SpawnreSources;
    }

    private void SpawnreSources(List<Resource> resourcesInObj, Vector3 position)
    {  
        foreach (Resource resource in resourcesInObj)
        {
            for (int i = 0; i < resource.amount; i++)
            {
                float chance = Random.Range(0, 100f);
                if (resource.dropChance >= chance)
                {
                    GameObject newResourceItem = Instantiate(resourceItemPrefab, position, Quaternion.identity);
                    Sprite newResourceimage = resourceSprites.Find(sprite => sprite.name == resource.type.ToString());
                    newResourceItem.GetComponent<ResourceItem>().SetParameters(resource.type, newResourceimage);
                }
            }
        }
    }    
}
public enum ResourceType
{
    Iron,
    Copper,
    Silver,
    Gold,
    Titanium
}


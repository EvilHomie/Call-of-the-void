using System.Collections.Generic;
using UnityEngine;

public class SpawnResourcesLogic : MonoBehaviour
{
    [SerializeField] List<Sprite> resourceSprites;
    [SerializeField] GameObject resourceItemPrefab;

    List<Resource> resInObj = new();

    private void OnEnable()
    {
        EventBus.onObjDie += SpawnreSources;
    }

    private void OnDisable()
    {
        EventBus.onObjDie -= SpawnreSources;
    }

    private void SpawnreSources(GameObject obj)
    {
        resInObj = obj.GetComponent<ResourcesInObject>().resourcesInObj;
        if (resInObj != null && resInObj.Count != 0)
        {
            foreach (Resource resource in resInObj)
            {
                for (int i = 0; i < resource.amount; i++)
                {
                    float chance = Random.Range(0, 100f);
                    if (resource.dropChance >= chance)
                    {
                        GameObject newResourceItem = Instantiate(resourceItemPrefab, obj.transform.position, Quaternion.identity);
                        Sprite newResourceimage = resourceSprites.Find(sprite => sprite.name == resource.type.ToString());
                        newResourceItem.GetComponent<ResourceItem>().SetParameters(resource.type, newResourceimage);
                    }
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


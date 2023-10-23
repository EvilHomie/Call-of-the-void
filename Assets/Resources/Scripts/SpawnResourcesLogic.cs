using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SpawnResourcesLogic : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    [SerializeField] List<Sprite> resourceSprites;
    [SerializeField] GameObject resourceItemPrefab;

    

    private void OnEnable()
    {
        EventBus.CommandOnObjDie.Subscribe(obj =>
        {
            obj.TryGetComponent(out ResourcesInObject resInObj);

            if (resInObj != null) 
            {                
                SpawnreSources(resInObj);
            }
            
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    private void SpawnreSources(ResourcesInObject resInObj)
    {
        List<Resource> resList = resInObj.resourcesInObj;
        if (resList.Count != 0)
        {
            foreach (Resource resource in resList)
            {
                for (int i = 0; i < resource.amount; i++)
                {
                    float chance = Random.Range(0, 100f);
                    if (resource.dropChance >= chance)
                    {
                        GameObject newResourceItem = Instantiate(resourceItemPrefab, resInObj.transform.position, Quaternion.identity);
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
    Titanium,
    Nothing
}


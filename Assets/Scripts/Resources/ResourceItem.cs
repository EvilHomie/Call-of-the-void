using UnityEngine;

public class ResourceItem : MonoBehaviour
{    
    SpriteRenderer resourceImage;
    public int resourceCount = 1;
    public ResourceType type;

    private void Awake()
    {
        resourceImage = GetComponentInChildren<SpriteRenderer>();
    }
    public void SetParameters (ResourceType resourceType, Sprite resourceImage)
    {        
        type = resourceType;
        this.resourceImage.sprite = resourceImage;        
    }
}



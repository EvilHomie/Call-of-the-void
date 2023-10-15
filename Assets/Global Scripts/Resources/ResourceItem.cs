using UnityEngine;

public class ResourceItem : MonoBehaviour
{    
    SpriteRenderer resourceRenderer;
    public int resourceCount = 1;
    public ResourceType type;
    public Sprite image;

    private void Awake()
    {
        resourceRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void SetParameters (ResourceType resourceType, Sprite resourceImage)
    {        
        type = resourceType;
        image = resourceImage;
        this.resourceRenderer.sprite = image;        
    }
}



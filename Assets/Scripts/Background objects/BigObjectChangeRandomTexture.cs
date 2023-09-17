using UnityEngine;

public class BigObjectChangeRandomTexture : MonoBehaviour
{
    [SerializeField] Texture[] bigObjectTextures;

    Renderer bigObjectRenderer;
    int randomTextureIndex;

    private void Awake()
    {
        bigObjectRenderer = GetComponent<Renderer>();
        randomTextureIndex = Random.Range(0, bigObjectTextures.Length);
        bigObjectRenderer.material.mainTexture = bigObjectTextures[randomTextureIndex];
    }
}

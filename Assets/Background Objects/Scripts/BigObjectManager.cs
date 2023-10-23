using UnityEngine;

public class BigObjectManager : MonoBehaviour
{
    [SerializeField] Texture[] bigObjectTextures;
    Renderer bigObjectRenderer;
    Rigidbody bigObjectRb;      

    readonly float minSize = 10f;
    readonly float maxSize = 100f;

    float moveSpeedBasedOnSize;
    readonly float distanceImitation = 10f;

    void OnEnable()
    {
        ChoiseTexture();
        RandomizeSize();
        GiveForceToMoveLeft();
    }

    private void OnDisable()
    {
        EventBus.ComandOnBigBGobjectDestroy.Execute();
    }

    void ChoiseTexture()
    {
        bigObjectRenderer = GetComponent<Renderer>();

        int randomTextureIndex;
        randomTextureIndex = UnityEngine.Random.Range(0, bigObjectTextures.Length);

        bigObjectRenderer.material.mainTexture = bigObjectTextures[randomTextureIndex];
    }

    void RandomizeSize()
    {
        float randomSize = UnityEngine.Random.Range(minSize, maxSize);
        moveSpeedBasedOnSize = randomSize / distanceImitation;
        transform.localScale = new Vector3(randomSize, 1, randomSize);
    }

    void GiveForceToMoveLeft()
    {
        bigObjectRb = GetComponent<Rigidbody>();
        bigObjectRb.AddForce(Vector3.left * moveSpeedBasedOnSize, ForceMode.VelocityChange);
    }
}

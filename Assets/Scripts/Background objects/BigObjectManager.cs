using System;
using System.Collections;
using UnityEngine;

public class BigObjectManager : MonoBehaviour
{
    [SerializeField] Texture[] bigObjectTextures;
    Renderer bigObjectRenderer;
    Rigidbody bigObjectRb;

    public static Action onBigObjectDestroy;
    
    readonly float maxDistanceFromPlayer = 1500f;

    readonly float minSize = 5f;
    readonly float maxSize = 100f;

    float moveSpeedBasedOnSize;
    readonly float distanceImitation = 10f;

    Vector3 playerPos;
    void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += GetPlayerPos;
        ChoiseTexture();
        RandomizeSize();
        GiveForceToMoveLeft();
        StartCoroutine(CheckDistance());
    }

    private void OnDisable()
    {
        PlayerControl.broadcastPlayerTransform -= GetPlayerPos;
        onBigObjectDestroy?.Invoke();
    }


    void GetPlayerPos(Transform playerTransform)
    {
        playerPos = playerTransform.position;
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

    IEnumerator CheckDistance()
    {
        while (true)
        {
            float curentDistanceFromPlayer = Vector3.Distance(transform.position, playerPos);
            if (curentDistanceFromPlayer > maxDistanceFromPlayer)
            {                
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}

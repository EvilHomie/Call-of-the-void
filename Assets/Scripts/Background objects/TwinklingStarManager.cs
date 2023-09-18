using System.Collections;
using UnityEngine;

public class TwinklingStarManager : MonoBehaviour
{
    Renderer starRender;
    Color starColor;
    
    float blinkSpeed;
    readonly float minBlinkSpeed = 1f;
    readonly float maxBlinkSpeed = 2f;

    float lifeTime;
    readonly float minLifeTime = 10f;
    readonly float maxLifeTime = 15f;

    float randomSize;
    readonly float minSize = 0.005f;
    readonly float maxSize = 0.01f;

    readonly float minBlinkBorderBeforeDestroy = 0.01f;

    private void Awake()
    {
        starRender = GetComponent<Renderer>();
        starColor = starRender.material.color;

        randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, 1, randomSize);

        

        StartCoroutine(nameof(DestroyStar));
    }

    private void FixedUpdate()
    {
        Pulsing();
    }

    void Pulsing()
    {
        blinkSpeed = Random.Range(minBlinkSpeed, maxBlinkSpeed);
        starColor.a = ((float)Mathf.Sin(Time.time * blinkSpeed) + 1) / 2;
        starRender.material.color = starColor;
    }

    IEnumerator DestroyStar()
    {
        lifeTime = Random.Range(minLifeTime, maxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        while (starColor.a > minBlinkBorderBeforeDestroy)
        {            
            yield return null;
        }
        Destroy(gameObject);
    }
}

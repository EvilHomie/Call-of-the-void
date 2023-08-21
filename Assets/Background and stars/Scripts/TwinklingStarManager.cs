using System;
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

        randomSize = UnityEngine.Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        

        StartCoroutine(nameof(DestroyStar));
    }

    private void Update()
    {
        Pulsing();
    }

    void Pulsing()
    {
        blinkSpeed = UnityEngine.Random.Range(minBlinkSpeed, maxBlinkSpeed);
        starColor.a = ((float)Math.Sin(Time.time * blinkSpeed) + 1) / 2;
        starRender.material.color = starColor;
    }

    IEnumerator DestroyStar()
    {
        lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        while (starColor.a > minBlinkBorderBeforeDestroy)
        {            
            yield return null;
        }
        Destroy(gameObject);
    }
}

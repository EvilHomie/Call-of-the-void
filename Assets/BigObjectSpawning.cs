using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigObjectSpawning : MonoBehaviour
{
    [SerializeField] GameObject bigObject;

    float delay;
    readonly float minDelay = 1;
    readonly float maxDelay = 1;

    readonly float spawnRadius = 800f;
    readonly float worldPozY = 5f;


    private void Start()
    {
        StartCoroutine();
        BigObjectManager.onBigObjectDestroy += StartCoroutine;
    }
    void OnDisable  ()
    {
        BigObjectManager.onBigObjectDestroy += StartCoroutine;
    }

    void StartCoroutine()
    {
        StartCoroutine(nameof(SpawnBigObject));
    }
    IEnumerator SpawnBigObject()
    {
        delay = Random.Range(minDelay, maxDelay);

        yield return new WaitForSeconds(delay);

        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos += transform.position;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * spawnRadius + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * spawnRadius + transform.position.z;
        randomPos.y = worldPozY;

        Instantiate(bigObject, randomPos, bigObject.transform.rotation);
    }    
}

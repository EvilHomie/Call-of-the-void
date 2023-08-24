using System.Collections;
using UnityEngine;

public class BigObjectSpawning : MonoBehaviour
{
    [SerializeField] GameObject bigObject;

    float spawnDelay = 1;
    readonly float minDelay = 1;
    readonly float maxDelay = 1;

    readonly float spawnRadiusAroundPlayer = 800f;

    void OnEnable()
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
        spawnDelay = Random.Range(minDelay, maxDelay);

        yield return new WaitForSeconds(spawnDelay);

        Vector3 randomPos = Random.insideUnitSphere * spawnRadiusAroundPlayer;
        randomPos += transform.position;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * spawnRadiusAroundPlayer + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * spawnRadiusAroundPlayer + transform.position.z;
        randomPos.y = transform.position.y;

        Instantiate(bigObject, randomPos, bigObject.transform.rotation);
    }    
}

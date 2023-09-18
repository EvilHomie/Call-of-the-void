using UnityEngine;

public class StarSpawning : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;

    float spawnDelay;
    [SerializeField] float minSpawnDelay;
    [SerializeField] float maxSpawnDelay;

    readonly float spawnPosXValue = 2.85f;
    readonly float spawnPosZValue = 1.6f;
    float bGScale;

    Vector3 spawnPos;

    void Start()
    {
        bGScale = transform.parent.localScale.x;
        RepitSpawn();
    }

    void StarSpawn()
    {
        float randomPosX = Random.Range(-spawnPosXValue, spawnPosXValue) * bGScale;
        float randomPosZ = Random.Range(-spawnPosZValue, spawnPosZValue) * bGScale;
        spawnPos = new Vector3(randomPosX, 0 , randomPosZ) + transform.position;

        Instantiate(starPrefab, spawnPos, starPrefab.transform.rotation, transform);
    }

    void RepitSpawn()
    {
        StarSpawn();
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke(nameof(RepitSpawn), spawnDelay);
    }
}

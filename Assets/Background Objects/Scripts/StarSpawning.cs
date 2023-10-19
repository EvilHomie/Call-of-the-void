using UnityEngine;

public class StarSpawning : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;

    float spawnDelay;
    [SerializeField] float minSpawnDelay;
    [SerializeField] float maxSpawnDelay;

    [SerializeField] float spawnPosXValue;
    [SerializeField] float spawnPosZValue;

    Vector3 spawnPos;

    void Start()
    {
        RepitSpawn();
    }

    void StarSpawn()
    {
        float randomPosX = Random.Range(-spawnPosXValue, spawnPosXValue);
        float randomPosZ = Random.Range(-spawnPosZValue, spawnPosZValue);
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

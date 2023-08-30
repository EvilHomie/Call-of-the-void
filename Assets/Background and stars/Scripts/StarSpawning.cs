using UnityEngine;

public class StarSpawning : MonoBehaviour
{
    [SerializeField] GameObject twinklingStar;

    float startSpawnDelay;
    readonly float minStartSpawnDelay = 2f;
    readonly float maxStartSpawnDelay = 5f;

    float repitSpawnDelay;
    readonly float minRepitSpawnDelay = 3f;
    readonly float maxRepitSpawnDelay = 5f;

    readonly float spawnPosXValue = 3.5f;
    readonly float spawnPosZValue = 2f;
    float bGScale;

    Vector3 spawnPos;

    void Start()
    {
        bGScale = transform.parent.localScale.x;

        startSpawnDelay = Random.Range(minStartSpawnDelay, maxStartSpawnDelay);
        Invoke(nameof(StarSpawn), startSpawnDelay);
    }

    void StarSpawn()
    {
        float randomPosX = Random.Range(-spawnPosXValue, spawnPosXValue) * bGScale;
        float randomPosZ = Random.Range(-spawnPosZValue, spawnPosZValue) * bGScale;
        spawnPos = new Vector3(randomPosX, 0 , randomPosZ) + transform.position;

        Instantiate(twinklingStar, spawnPos, twinklingStar.transform.rotation, transform);

        repitSpawnDelay = Random.Range(minRepitSpawnDelay, maxRepitSpawnDelay);
        Invoke(nameof(StarSpawn), repitSpawnDelay);
    }
}

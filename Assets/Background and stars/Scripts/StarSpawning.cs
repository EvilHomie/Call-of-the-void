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

    readonly float spawnPosXValue = 4.4f;
    readonly float spawnPosZValue = 2.1f;
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
        float spawnPosX = Random.Range(-spawnPosXValue, spawnPosXValue) * bGScale;
        float spawnPosZ = Random.Range(-spawnPosZValue, spawnPosZValue) * bGScale;
        spawnPos = new Vector3(spawnPosX, transform.position.y , spawnPosZ);

        Instantiate(twinklingStar, spawnPos, twinklingStar.transform.rotation, transform);

        repitSpawnDelay = Random.Range(minRepitSpawnDelay, maxRepitSpawnDelay);
        Invoke(nameof(StarSpawn), repitSpawnDelay);
    }
}

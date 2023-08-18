using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarSpawning : MonoBehaviour
{
    [SerializeField] GameObject twinklingStar;

    float startSpawnDelay;
    readonly float minStartSpawnDelay = 2f;
    readonly float maxStartSpawnDelay = 5f;

    float repitSpawnDelay;
    readonly float minRepitSpawnDelay = 2f;
    readonly float maxRepitSpawnDelay = 10f;

    readonly float spawnPosXValue = 4.8f;
    readonly float spawnPosYValue = 2.6f;
    float bGScale;

    Vector3 spawnPos;

    void Start()
    {
        bGScale = transform.parent.localScale.x;
        Debug.Log(bGScale);
        startSpawnDelay = Random.Range(minStartSpawnDelay, maxStartSpawnDelay);
        Invoke(nameof(StarSpawn), startSpawnDelay);
    }

    void StarSpawn()
    {
        float spawnPosX = Random.Range(-spawnPosXValue, spawnPosXValue) * bGScale;
        float spawnPosY = Random.Range(-spawnPosYValue, spawnPosYValue) * bGScale;
        spawnPos = new Vector3(spawnPosX, spawnPosY, -1);

        Instantiate(twinklingStar, spawnPos, twinklingStar.transform.rotation, transform);

        repitSpawnDelay = Random.Range(minRepitSpawnDelay, maxRepitSpawnDelay);

        Invoke(nameof(StarSpawn), repitSpawnDelay);
    }
}

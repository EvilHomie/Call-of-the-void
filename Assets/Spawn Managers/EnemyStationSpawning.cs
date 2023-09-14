using System;
using UnityEngine;

public class EnemyStationSpawning : MonoBehaviour
{
    [SerializeField] GameObject[] stationPrefabs;
    public static Action<Vector3> broadcastStationPosition;

    GameObject player;

    float spawnDelay;
    float minDelay = 2;
    float maxDelay = 2;

    float spawnAngle = 45;
    readonly float spawnDistance = 500f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnDelay = UnityEngine.Random.Range(minDelay, maxDelay);
        Invoke(nameof(SpawnEnemyStation), spawnDelay);
    }

    void SpawnEnemyStation()
    {  
        if (GameObject.FindGameObjectWithTag("Station") != null)
        {
            spawnDelay = UnityEngine.Random.Range(minDelay, maxDelay);
            Invoke(nameof(SpawnEnemyStation), spawnDelay);
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, stationPrefabs.Length);
            float randomAngle = UnityEngine.Random.Range(-spawnAngle, spawnAngle);

            Vector3 Pos = Quaternion.Euler(0, player.transform.eulerAngles.y + randomAngle, 0) * transform.forward * spawnDistance + player.transform.position;
            Instantiate(stationPrefabs[randomIndex], Pos, Quaternion.identity);
            broadcastStationPosition?.Invoke(Pos);

            spawnDelay = UnityEngine.Random.Range(minDelay, maxDelay);
            Invoke(nameof(SpawnEnemyStation), spawnDelay);
        }        
    }
}

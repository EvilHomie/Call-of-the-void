using UnityEngine;

public class EnemyStationSpawning : MonoBehaviour
{
    [SerializeField] GameObject[] stationPrefabs;

    GameObject player;

    float spawnDelay;
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;

    float spawnAngle = 45;
    readonly float spawnDistance = 500f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnDelay = UnityEngine.Random.Range(minDelay, maxDelay);
        Invoke(nameof(SpawnEnemyStation), spawnDelay);
    }

    private void OnEnable()
    {
        BroadcastStationPosition.broadcastStationPosition += Invoke;
    }

    private void OnDisable()
    {
        BroadcastStationPosition.broadcastStationPosition -= Invoke;
    }
   
    void SpawnEnemyStation()
    {
        int randomIndex = UnityEngine.Random.Range(0, stationPrefabs.Length);
        float randomAngle = UnityEngine.Random.Range(-spawnAngle, spawnAngle);

        Vector3 Pos = Quaternion.Euler(0, player.transform.eulerAngles.y + randomAngle, 0) * transform.forward * spawnDistance + transform.position;
        Instantiate(stationPrefabs[randomIndex], Pos, Quaternion.identity);             
    }

    void Invoke(Vector3 stationPos)
    {
        if (stationPos == Vector3.zero)
        {
            spawnDelay = UnityEngine.Random.Range(minDelay, maxDelay);
            Invoke(nameof(SpawnEnemyStation), spawnDelay);
        }
    }
}

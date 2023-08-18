using UnityEngine;

public class AsteroidSpawning1 : MonoBehaviour
{
    public GameObject player;
    public GameObject[] asteroid;

    readonly float distance = 120f;

    float startSpawnDelay;
    readonly float minStartSpawnDelay = 1f;
    readonly float maxStartSpawnDelay = 5f;

    float repitSpawnDelay;
    readonly float minRepitSpawnDelay = 3f;
    readonly float maxRepitSpawnDelay = 10f;

    private void Start()
    {
        startSpawnDelay = Random.Range(minStartSpawnDelay, maxStartSpawnDelay);
        Invoke(nameof(AsteroidSpawn), startSpawnDelay);
    }
    private void Update()
    {
        PlayerFollow();
    }


    void AsteroidSpawn()
    {
        int randomAsterIndex = Random.Range(0, asteroid.Length);

        Vector3 randomPos = Random.insideUnitSphere * distance;
        randomPos += transform.position;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * distance + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * distance + transform.position.y;
        randomPos.z = transform.position.z;

        GameObject go = Instantiate(asteroid[randomAsterIndex], randomPos, Quaternion.identity);
        go.transform.position = randomPos;

        repitSpawnDelay = Random.Range(minRepitSpawnDelay, maxRepitSpawnDelay);
        Invoke(nameof(AsteroidSpawn), repitSpawnDelay);
    }

    void PlayerFollow()
    {
        transform.position = player.transform.position;
    }
}

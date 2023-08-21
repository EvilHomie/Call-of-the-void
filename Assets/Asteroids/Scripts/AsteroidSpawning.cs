using UnityEngine;

public class AsteroidSpawning1 : MonoBehaviour
{
    [SerializeField] GameObject[] asteroid;

    readonly float spawnRadius = 200f;

    float startSpawnDelay;
    readonly float minStartSpawnDelay = 1f;
    readonly float maxStartSpawnDelay = 5f;

    float repitSpawnDelay;
    readonly float minRepitSpawnDelay = 2f;
    readonly float maxRepitSpawnDelay = 6f;

    private void Start()
    {
        startSpawnDelay = Random.Range(minStartSpawnDelay, maxStartSpawnDelay);
        Invoke(nameof(AsteroidSpawn), startSpawnDelay);
    }
    
    void AsteroidSpawn()
    {
        int randomAsterIndex = Random.Range(0, asteroid.Length);

        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos += transform.position;
        //randomPos.y = transform.position.y;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * spawnRadius + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * spawnRadius + transform.position.z;
        randomPos.y = transform.position.y;

        Instantiate(asteroid[randomAsterIndex], randomPos, Quaternion.identity);

        repitSpawnDelay = Random.Range(minRepitSpawnDelay, maxRepitSpawnDelay);
        Invoke(nameof(AsteroidSpawn), repitSpawnDelay);
    }
}

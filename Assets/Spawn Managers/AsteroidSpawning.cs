using UnityEngine;

public class AsteroidSpawning : MonoBehaviour
{
    [SerializeField] GameObject[] asteroid;

    readonly float spawnRadiusAroundPlayer = 200f;
    readonly float spawnDelay = 1.5f;
    readonly float lowerLimitPlayerSpeed = 20f;
    readonly float spawnDistanceAheadPlayerMod = 5f;
    readonly float spawnRadiusAheadPlayer = 100f;
    float playerSpeed = 0;
    Vector3 spawnPosAheadPlayer;

    private void OnEnable()
    {
        Invoke(nameof(SpawnAsteroidsMethodName), spawnDelay);
        PlayerControl.broadcastPlayerVelocity += PlayerTrañking;
    }

    private void OnDisable()
    {
        PlayerControl.broadcastPlayerVelocity -= PlayerTrañking;
    }        

    void SpawnAsteroidsMethodName()
    {
        if (playerSpeed > lowerLimitPlayerSpeed)
        {
            AsteroidSpawnWhenPlayerMove();   
        }
        else
        {
            AsteroidSpawnWhenPlayerStay();
        }
    }

    void PlayerTrañking(Vector3 playerVelocity)
    {
        playerSpeed = playerVelocity.magnitude;
        spawnPosAheadPlayer = playerVelocity;
    }

    void AsteroidSpawnWhenPlayerStay()
    {      
        Vector3 randomPos = Random.insideUnitSphere * spawnRadiusAroundPlayer;
        randomPos += transform.position;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * spawnRadiusAroundPlayer + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * spawnRadiusAroundPlayer + transform.position.z;
        randomPos.y = transform.position.y;

        Instantiate(asteroid[RandomAsteroidIndex()], randomPos, Quaternion.identity);

        Invoke(nameof(SpawnAsteroidsMethodName), spawnDelay);
    }

    void AsteroidSpawnWhenPlayerMove()
    {
        Vector3 Pos = new Vector3 (Random.insideUnitCircle.x, 0 , Random.insideUnitCircle.y) * spawnRadiusAheadPlayer + transform.position + spawnPosAheadPlayer * spawnDistanceAheadPlayerMod;
        Instantiate(asteroid[RandomAsteroidIndex()], Pos, Quaternion.identity);
        Invoke(nameof(SpawnAsteroidsMethodName), spawnDelay);
    }

    int RandomAsteroidIndex()
    {
        int randomAsterIndex = Random.Range(0, asteroid.Length);
        return randomAsterIndex;
    }
}

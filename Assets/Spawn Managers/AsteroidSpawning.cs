using UnityEngine;

public class AsteroidSpawning : MonoBehaviour
{
    [SerializeField] GameObject[] asteroid;

    readonly float radiusAroundPlayer = 200f;
    readonly float spawnDelay = 5f;
    readonly float lowerValuePlyerSpeed = 10f;
    readonly float distanceMod = 15f;
    readonly float radiusAheadPlayer = 100f;
    float playerSpeed = 0;
    Vector3 playerMoveDirection;

    void OnEnable()
    {
        Invoke(nameof(SpawnAsteroidsMethodName), spawnDelay);
        PlayerControl.broadcastPlayerVelocity += PlayerTrañking;
    }

    void OnDisable()
    {
        PlayerControl.broadcastPlayerVelocity -= PlayerTrañking;
    }        

    void SpawnAsteroidsMethodName()
    {
        if (playerSpeed > lowerValuePlyerSpeed)
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
        playerMoveDirection = playerVelocity;
    }

    void AsteroidSpawnWhenPlayerStay()
    {      
        Vector3 randomPos = Random.insideUnitSphere * radiusAroundPlayer;
        randomPos += transform.position;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radiusAroundPlayer + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radiusAroundPlayer + transform.position.z;
        randomPos.y = transform.position.y;

        Instantiate(asteroid[RandomAsteroidIndex()], randomPos, Quaternion.identity);

        Invoke(nameof(SpawnAsteroidsMethodName), spawnDelay);
    }

    void AsteroidSpawnWhenPlayerMove()
    {
        Vector3 Pos = new Vector3 (Random.insideUnitCircle.x, 0 , Random.insideUnitCircle.y) * radiusAheadPlayer + transform.position + playerMoveDirection * distanceMod;
        Instantiate(asteroid[RandomAsteroidIndex()], Pos, Quaternion.identity);
        Invoke(nameof(SpawnAsteroidsMethodName), spawnDelay);
    }

    int RandomAsteroidIndex()
    {
        int randomAsterIndex = Random.Range(0, asteroid.Length);
        return randomAsterIndex;
    }
}

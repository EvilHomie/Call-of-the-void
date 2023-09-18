using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] objectsPrefabs;

    [SerializeField] float spawnRadius;    
    [SerializeField] float lowerValuePlayerVelocity;
    [SerializeField] float spawnAngleWhenPlayerIsMoving;
    [SerializeField] protected float minSpawnDelay;
    [SerializeField] protected float maxSpawnDelay;

    [SerializeField] protected float spawnDelay;
    float playerVelocity = 0;
    Transform playerTransform = null;

    protected virtual void OnEnable()
    {
        PlayerControl.broadcastPlayerVelocity += GetPlayerVelocity;
        PlayerControl.broadcastPlayerTransform += GetPlayerTransform;
    }

    protected virtual void OnDisable()
    {
        PlayerControl.broadcastPlayerVelocity -= GetPlayerVelocity;
        PlayerControl.broadcastPlayerTransform -= GetPlayerTransform;
    }

    protected void ChoiseSpawnMethod()
    {
        if (playerVelocity > lowerValuePlayerVelocity)
        {
            SpawnWhenPlayerMove();   
        }
        else
        {
            SpawnWhenPlayerStay();
        }
    }

    protected void GetPlayerVelocity(Vector3 playerVelocity)
    {
        this.playerVelocity = playerVelocity.magnitude;
    }

    protected void GetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    void SpawnWhenPlayerStay()
    {      
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos += transform.position;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * spawnRadius + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * spawnRadius + transform.position.z;
        randomPos.y = transform.position.y;

        Instantiate(objectsPrefabs[RandomObjectIndex()], randomPos, Quaternion.identity);
    }

    void SpawnWhenPlayerMove()
    {
        float randomAngle = Random.Range(-spawnAngleWhenPlayerIsMoving, spawnAngleWhenPlayerIsMoving);

        Vector3 Pos = Quaternion.Euler(0, playerTransform.eulerAngles.y + randomAngle, 0) * transform.forward * spawnRadius + transform.position;
        Instantiate(objectsPrefabs[RandomObjectIndex()], Pos, Quaternion.identity);
    }

    int RandomObjectIndex()
    {
        int randomAsterIndex = Random.Range(0, objectsPrefabs.Length);
        return randomAsterIndex;
    }
}

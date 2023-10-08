using UnityEngine;

public class SpawnManagerLogic : MonoBehaviour
{
    [SerializeField] GameObject[] objectsPrefabs;

    [SerializeField] float spawnRadius;    
    [SerializeField] float lowerValuePlayerVelocity;
    [SerializeField] float spawnAngleWhenPlayerIsMoving;
    [SerializeField] protected float minSpawnDelay;
    [SerializeField] protected float maxSpawnDelay;

    [SerializeField] protected float spawnDelay;

    protected void ChoiseSpawnMethod()
    {
        if (GlobalData.playerVelocity.magnitude > lowerValuePlayerVelocity)
        {
            SpawnWhenPlayerMove();   
        }
        else
        {
            SpawnWhenPlayerStay();
        }
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

        Vector3 Pos = Quaternion.Euler(0, GlobalData.playerTransform.eulerAngles.y + randomAngle, 0) * transform.forward * spawnRadius + transform.position;
        Instantiate(objectsPrefabs[RandomObjectIndex()], Pos, Quaternion.identity);
    }

    int RandomObjectIndex()
    {
        int randomAsterIndex = Random.Range(0, objectsPrefabs.Length);
        return randomAsterIndex;
    }
}

using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    Rigidbody asteroidRB;   

    float moveSpeed;
    readonly float minMoveSpeed = 3;
    readonly float maxMoveSpeed = 10;

    float tumbleSpeed;
    readonly float minTumbleSpeed = 0.3f;
    readonly float maxTumbleSpeed = 1.0f;

    readonly float radiusAroundPlayer = 100f;
    
    Vector3 playerPos;    

    void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += GetPlayerPos;
        asteroidRB = GetComponent<Rigidbody>();        

        RandomRotator();
        MoveToGameZone();        
    }

    private void OnDestroy()
    {
        PlayerControl.broadcastPlayerTransform -= GetPlayerPos;
    }

    void GetPlayerPos(Transform playerTransform)
    {
        playerPos = playerTransform.position;
    }
    void RandomRotator()
    {
        tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed);
        asteroidRB.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
    }

    void MoveToGameZone()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        asteroidRB.AddForce(DirectionCoordonates() * moveSpeed, ForceMode.VelocityChange);
    }

    Vector3 DirectionCoordonates()
    {
        Vector3 randomPointPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * radiusAroundPlayer + playerPos;
        Vector3 direction = randomPointPos - transform.position;
        return direction.normalized;
    }  
}

using System.Collections;
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

    float curentDistanceFromPlayer;
    readonly float maxDistanceFromPlayer = 800f;
    Vector3 playerPos;

    void Awake()
    {
        PlayerControl.broadcastPlayerTransform += GetPlayerPos;
        asteroidRB = GetComponent<Rigidbody>();

        RandomRotator();
        MoveToGameZone();
        StartCoroutine(nameof(CheckDistance));
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

    IEnumerator CheckDistance()
    {
        while (true)
        {
            curentDistanceFromPlayer = Vector3.Distance(transform.position, playerPos);
            if (curentDistanceFromPlayer > maxDistanceFromPlayer)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(2f);
        }  
    }
}

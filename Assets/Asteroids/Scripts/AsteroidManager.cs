using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    GameObject asteroidsSpawnManager;
    Rigidbody asteroidRB;
    
    float speed;
    readonly float minSpeed = 0.05f;
    readonly float maxSpeed = 0.15f;

    float tumbleSpeed;
    readonly float minTumbleSpeed = 0.25f;
    readonly float maxTumbleSpeed = 1.0f;

    readonly float radiusGameZone = 150f;
    Vector3 direction;

    float curentDistanceFromSpawnPoint;
    readonly float maxDistanceFromSpawnPoint = 400f;

    void Awake()
    {
        asteroidsSpawnManager = FindObjectOfType<AsteroidSpawning>().gameObject;
        asteroidRB = GetComponent<Rigidbody>();

        MoveToGameZone();
        StartCoroutine(nameof(CheckDistance));
    }

    void RandomRotator()
    {
        tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed);
        asteroidRB.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
    }

    void MoveToGameZone()
    {
        asteroidRB.AddForce(DirectionCoordonates() * SpeedCalc(), ForceMode.VelocityChange);
    }

    Vector3 DirectionCoordonates()
    {
        Vector3 randomDirectionPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * radiusGameZone + asteroidsSpawnManager.transform.position;
        direction = randomDirectionPos - transform.position;
        return direction;
    }

    float SpeedCalc()
    {
        speed = Random.Range(minSpeed,maxSpeed);
        return speed;
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            curentDistanceFromSpawnPoint = Vector3.Distance(transform.position, asteroidsSpawnManager.transform.position);
            if (curentDistanceFromSpawnPoint > maxDistanceFromSpawnPoint)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(2f);
        }  
    }
}

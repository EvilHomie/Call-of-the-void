using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    GameObject asteroidsSpawnManager;
    Rigidbody asteroidRB;
    

    float speed;
    readonly float minSpeed = 0.03f;
    readonly float maxSpeed = 0.05f;

    float tumbleSpeed;
    readonly float minTumbleSpeed = 0.25f;
    readonly float maxTumbleSpeed = 1.0f;

    readonly float radiusGameZone = 100f;
    Vector3 direction;

    float curentDistanceFromSpawnPoint;
    readonly float maxDistanceFromSpawnPoint = 800f;

    void Awake()
    {
        asteroidsSpawnManager = FindObjectOfType<AsteroidSpawning>().gameObject;
        asteroidRB = GetComponent<Rigidbody>();

        RandomRotator();
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
        speed = Random.Range(minSpeed, maxSpeed);
        asteroidRB.AddForce(DirectionCoordonates() * speed, ForceMode.VelocityChange);
    }

    Vector3 DirectionCoordonates()
    {
        Vector3 randomDirectionPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * radiusGameZone + asteroidsSpawnManager.transform.position;
        direction = randomDirectionPos - transform.position;
        return direction;
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

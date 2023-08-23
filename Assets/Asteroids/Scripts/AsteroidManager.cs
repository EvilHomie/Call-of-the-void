using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    GameObject asteroidsSpawnManager;
    Rigidbody asteroidRB;
    
    float speed;
    readonly float minSpeed = 0.05f;
    readonly float maxSpeed = 0.15f;

    readonly float radiusGameZone = 150f;
    Vector3 direction;
    //float asteroidsSpawnPointPosY;

    float curentDistanceFromSpawnPoint;
    readonly float maxDistanceFromSpawnPoint = 400f;

    void Awake()
    {
        asteroidsSpawnManager = GameObject.Find("Asteroids Spawn Manager");
        asteroidRB = GetComponent<Rigidbody>();

        //asteroidsSpawnPointPosY = asteroidsSpawnManager.transform.position.y;
        MoveToGameZone();
        StartCoroutine(nameof(DestroyAsteroid));
        //StartCoroutine(nameof(SaveYPos));
    }

    //IEnumerator SaveYPos()
    //{
    //    while (true)
    //    {
    //        if (transform.position.y != asteroidsSpawnPointPosY)
    //        {
    //            transform.position = new Vector3(transform.position.x, asteroidsSpawnPointPosY, transform.position.z);
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

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


    IEnumerator DestroyAsteroid()
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

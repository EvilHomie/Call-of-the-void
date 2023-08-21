using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    
    float speed;
    readonly float minSpeed = 0.05f;
    readonly float maxSpeed = 0.15f;

    readonly float radiusGameZone = 150f;
    Vector3 direction;
    float playerYPos;

    float curentDistanceFromThePlayer;
    readonly float maxDistanceFromThePlayer = 400f;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();

        playerYPos = player.transform.position.y;
        MoveToGameZone();
        StartCoroutine(nameof(DestroyAsteroid));
        StartCoroutine(nameof(SaveYPos));
    }

    IEnumerator SaveYPos()
    {
        while (true)
        {
            if (transform.position.y != playerYPos)
            {
                transform.position = new Vector3(transform.position.x, playerYPos, transform.position.z);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void MoveToGameZone()
    {
        rb.AddForce(DirectionCoordonates() * SpeedCalc(), ForceMode.VelocityChange);
    }

    Vector3 DirectionCoordonates()
    {
        Vector3 randomDirectionPos = new Vector3(Random.insideUnitCircle.x, player.transform.position.y, Random.insideUnitCircle.y) * radiusGameZone + player.transform.position;
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
            curentDistanceFromThePlayer = Vector3.Distance(transform.position, player.transform.position);
            if (curentDistanceFromThePlayer > maxDistanceFromThePlayer)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(2f);
        }  
    }
}

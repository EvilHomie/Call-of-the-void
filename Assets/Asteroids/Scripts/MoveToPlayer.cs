using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    
    float speed;
    readonly float minSpeed = 0.05f;
    readonly float maxSpeed = 0.15f;

    readonly float radiusAroundPlayer = 50f;

    Vector3 direction;
    float constZPos;
    float curentDistanceFromThePlayer;
    readonly float maxDistanceFromThePlayer = 200;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();

        constZPos = player.transform.position.z;
        AddForce();
    }

    private void Update()
    {
        ConstPosZ();
        DestroyAsteroid();
    }
    Vector3 DirectionCoordonates()
    {        
        Vector2 randomDirectionPos = Random.insideUnitCircle * radiusAroundPlayer + (Vector2)player.transform.position;
        direction = randomDirectionPos - (Vector2)transform.position;
        return direction;
    }

    void ConstPosZ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, constZPos);
    }

    void AddForce()
    {
        rb.AddForce(DirectionCoordonates() * SpeedCalc(), ForceMode.VelocityChange);
    }

    void DestroyAsteroid()
    {
        curentDistanceFromThePlayer = Vector3.Distance(transform.position, player.transform.position);
        if (curentDistanceFromThePlayer > maxDistanceFromThePlayer)
        {
            Destroy(gameObject);
        }
    }

    float SpeedCalc()
    {
        speed = Random.Range(minSpeed,maxSpeed);
        return speed;
    }
}

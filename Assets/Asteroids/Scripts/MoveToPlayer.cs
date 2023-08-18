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

    readonly float radiusAroundPlayer = 150f;

    Vector3 direction;
    float constYPos;
    float curentDistanceFromThePlayer;
    readonly float maxDistanceFromThePlayer = 300f;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();

        constYPos = player.transform.position.y;
        AddForceToMoove();
    }

    private void Update()
    {
        ConstPosY();
        DestroyAsteroid();
    }
    Vector3 DirectionCoordonates()
    {        
        Vector3 randomDirectionPos = new Vector3(Random.insideUnitCircle.x, player.transform.position.y , Random.insideUnitCircle.y) * radiusAroundPlayer + player.transform.position;
        direction = randomDirectionPos - transform.position;
        return direction;
    }

    void ConstPosY()
    {
        transform.position = new Vector3(transform.position.x, constYPos, transform.position.z);
    }

    void AddForceToMoove()
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

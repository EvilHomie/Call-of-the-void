using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BigObjectManager : MonoBehaviour
{
    // закоментировал возможность планеты двигаться вместе с игроком (для имитации влияния отдалености планеты на скорость корабля)
    GameObject player;
    Rigidbody rb;

    public static Action onBigObjectDestroy;

    float curentDistanceFromThePlayer;
    readonly float maxDistanceFromThePlayer = 1000f;

    float randomSize;
    readonly float minSize = 5f;
    readonly float maxSize = 100f;

    float constMoveSpeedLikeDistance;
    readonly float distanceImitation = 200f;

    //float differenceSpeedWithPlayer;
    //readonly float minDifference = 1;
    //readonly float maxDifference = 1;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();

        RandomizeSize();

        StartCoroutine(nameof(BigObjectDestroy));

        //differenceSpeedWithPlayer = UnityEngine.Random.Range(minDifference, maxDifference);
        //PlayerControl.playerVelocity += BigObjectMoveWithPlayer;

    }

    void FixedUpdate()
    {
        ConstantMoveToLeft();
    }
    IEnumerator BigObjectDestroy()
    {
        while (true)
        {
            curentDistanceFromThePlayer = Vector3.Distance(transform.position, player.transform.position);
            if (curentDistanceFromThePlayer > maxDistanceFromThePlayer)
            {
                onBigObjectDestroy?.Invoke();
                //PlayerControl.playerVelocity -= BigObjectMoveWithPlayer;
                Destroy(gameObject);    
            }
            yield return new WaitForSeconds(2f);
        }
    }

    void RandomizeSize()
    {
        randomSize = UnityEngine.Random.Range(minSize, maxSize);
        constMoveSpeedLikeDistance = randomSize / distanceImitation;
        transform.localScale = new Vector3(randomSize, 1, randomSize);
    }

    //void BigObjectMoveWithPlayer(Vector3 playerVelocity)
    //{
    //    rb.velocity = playerVelocity * differenceSpeedWithPlayer;
    //}

    void ConstantMoveToLeft()
    {        
        transform.position = new Vector3(transform.position.x - constMoveSpeedLikeDistance, transform.position.y, transform.position.z);
    }
}

using System;
using System.Collections;
using UnityEngine;

public class BigObjectManager : MonoBehaviour
{
    // закоментировал возможность планеты двигаться вместе с игроком (для имитации влияния отдалености планеты на скорость корабля)
    GameObject asteroidsSpawnManager;
    //Rigidbody rb;

    public static Action onBigObjectDestroy;

    float curentDistanceFromSpawnPoint;
    readonly float maxDistanceFromSpawnPoint = 2000f;

    float randomSize;
    readonly float minSize = 5f;
    readonly float maxSize = 100f;

    float constMoveSpeedBasedOnSize;
    readonly float distanceImitation = 10f;

    //float differenceSpeedWithPlayer;
    //readonly float minDifference = 1;
    //readonly float maxDifference = 1;

    void Awake()
    {
        asteroidsSpawnManager = GameObject.Find("Asteroids Spawn Manager");
        //rb = GetComponent<Rigidbody>();

        RandomizeSize();

        StartCoroutine(nameof(BigObjectDestroy));

        //differenceSpeedWithPlayer = UnityEngine.Random.Range(minDifference, maxDifference);
        //PlayerControl.playerVelocity += BigObjectMoveWithPlayer;

    }

    void Update()
    {
        ConstantMoveToLeft();
    }
    IEnumerator BigObjectDestroy()
    {
        while (true)
        {
            curentDistanceFromSpawnPoint = Vector3.Distance(transform.position, asteroidsSpawnManager.transform.position);
            if (curentDistanceFromSpawnPoint > maxDistanceFromSpawnPoint)
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
        constMoveSpeedBasedOnSize = randomSize / distanceImitation;
        transform.localScale = new Vector3(randomSize, 1, randomSize);
    }

    //void BigObjectMoveWithPlayer(Vector3 playerVelocity)
    //{
    //    rb.velocity = playerVelocity * differenceSpeedWithPlayer;
    //}

    void ConstantMoveToLeft()
    {        
        transform.position = new Vector3(transform.position.x - constMoveSpeedBasedOnSize * Time.deltaTime, transform.position.y, transform.position.z);
    }
}

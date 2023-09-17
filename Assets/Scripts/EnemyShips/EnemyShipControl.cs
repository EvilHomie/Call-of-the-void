using System.Collections;
using UnityEngine;

public class EnemyShipControl : MonoBehaviour
{
    Rigidbody enemyRb;
    Vector3 playerPos;
    float currentDistanceFromPlayer;
    float changeDirectionDelay;
    int[] sideMod = {-1, 1 };
    int sideModRandomIndex;

    [SerializeField] float mainEngineSpeed;
    [SerializeField] float mainEngineSpeedminValue;
    [SerializeField] float mainEngineSpeedmaxValue;

    [SerializeField] float enemyRotateSpeed;
    [SerializeField] float enemyRotateSpeedMinValue;
    [SerializeField] float enemyRotateSpeedMaxValue;

    [SerializeField] float enemyRCSSpeed;
    [SerializeField] float rCSpeedMinValue;
    [SerializeField] float rCSpeedMaxValue;

    [SerializeField] float maxDistaceFromPlayer;
    [SerializeField] float maxDistaceFromPlayerMinValue;
    [SerializeField] float maxDistaceFromPlayerMaxValue;

    [SerializeField] float minDistaceFromPlayer;
    [SerializeField] float minDistaceFromPlayerMinValue;
    [SerializeField] float minDistaceFromPlayerMaxValue;
    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        SetParameters();
        PlayerControl.broadcastPlayerPosition += GetPlayerPos;
        StartCoroutine(ChangeSideDirection());
    }

    private void OnDestroy()
    {
        PlayerControl.broadcastPlayerPosition -= GetPlayerPos;
    }
    private void FixedUpdate()
    {
        currentDistanceFromPlayer = Vector3.Distance(playerPos, transform.position);

        RotateToplayer();
        MoveToPlayer();
    }

    private void GetPlayerPos(Vector3 playerPos)
    {
        this.playerPos = playerPos;
    }

    void SetParameters()
    {
        minDistaceFromPlayer = Random.Range(minDistaceFromPlayerMinValue, minDistaceFromPlayerMaxValue);
        maxDistaceFromPlayer = Random.Range(maxDistaceFromPlayerMinValue, maxDistaceFromPlayerMaxValue);
        mainEngineSpeed = Random.Range(mainEngineSpeedminValue, mainEngineSpeedmaxValue);
        enemyRotateSpeed = Random.Range(enemyRotateSpeedMinValue, enemyRotateSpeedMaxValue);
        enemyRCSSpeed = Random.Range(rCSpeedMinValue, rCSpeedMaxValue);
    }

    private void RotateToplayer()
    {
        if (currentDistanceFromPlayer < maxDistaceFromPlayer)
        {
            Vector3 playerDirection = playerPos - transform.position;
            Quaternion toPlayer = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toPlayer, enemyRotateSpeed * Time.deltaTime);
        }        
    }

    void MoveToPlayer()
    {
        if (currentDistanceFromPlayer < maxDistaceFromPlayer & currentDistanceFromPlayer > (maxDistaceFromPlayer + minDistaceFromPlayer)/2)
        {
            enemyRb.AddForce(transform.forward * mainEngineSpeed, ForceMode.Acceleration);
        }

        if (currentDistanceFromPlayer < minDistaceFromPlayer)
        {
            enemyRb.AddForce(sideMod[sideModRandomIndex] * transform.forward * enemyRCSSpeed, ForceMode.Acceleration);
        }

        if (currentDistanceFromPlayer < (maxDistaceFromPlayer + minDistaceFromPlayer) / 2)
        {
            enemyRb.AddForce(sideMod[sideModRandomIndex] * transform.right * enemyRCSSpeed, ForceMode.Acceleration);
        }        
    }
    IEnumerator ChangeSideDirection()
    {
        changeDirectionDelay = Random.Range(3f, 10f);
        yield return new WaitForSeconds(changeDirectionDelay);
        sideModRandomIndex = Random.Range(0, sideMod.Length);
        StartCoroutine(ChangeSideDirection());
    }
}
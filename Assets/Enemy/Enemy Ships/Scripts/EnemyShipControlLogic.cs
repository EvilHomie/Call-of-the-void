using UnityEngine;

public class EnemyShipControlLogic : MonoBehaviour
{
    Rigidbody enemyRb;
    [SerializeField] bool playerInRange = false;
    [SerializeField] float checkControlZoneDelay;
    [SerializeField] int sideDirectionMod;

    [SerializeField] float mainEngineSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float rCSSpeed;
    [SerializeField] float controlZoneRadius;
    [SerializeField] float weaponDistance;
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(CheckControlZone), 1, checkControlZoneDelay);
    }

    void FixedUpdate()
    {
        if (playerInRange)
        {
            RotateToplayer();
            MoveToPlayer();            
        }
    }

    void CheckControlZone()
    {
        playerInRange = Vector3.Distance(GlobalData.PlayerTransform.position, transform.position) <= controlZoneRadius;
        if (playerInRange)
            ChangeSideDirection();
    }
    

    public void SetParameters(float mainEngineSpeed, float rotateSpeed, float rCSSpeed, float controlZoneRadius, float weaponDistance, float checkControlZoneDelay)
    {
        this.mainEngineSpeed = mainEngineSpeed;
        this.rotateSpeed = rotateSpeed;
        this.rCSSpeed = rCSSpeed;
        this.controlZoneRadius = controlZoneRadius;
        this.weaponDistance = weaponDistance;
        this.checkControlZoneDelay = checkControlZoneDelay;
    }

    void RotateToplayer()
    {
        Vector3 playerDirection = GlobalData.PlayerTransform.position - transform.position;
        Quaternion toPlayer = Quaternion.LookRotation(playerDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toPlayer, rotateSpeed * Time.deltaTime);
    }

    void MoveToPlayer()
    {
        float currentDistanceFromPlayer = Vector3.Distance(GlobalData.PlayerTransform.position, transform.position);

        if (currentDistanceFromPlayer > weaponDistance && currentDistanceFromPlayer > weaponDistance / 2)
        {
            enemyRb.AddForce(transform.forward * mainEngineSpeed, ForceMode.Acceleration);
        }

        if (currentDistanceFromPlayer < weaponDistance / 2 )
        {
            enemyRb.AddForce(-transform.forward * rCSSpeed, ForceMode.Acceleration);
        }

        if (currentDistanceFromPlayer < weaponDistance)
        {
            enemyRb.AddForce(sideDirectionMod * rCSSpeed * transform.right, ForceMode.Acceleration);
        }
    }
    void ChangeSideDirection()
    {
        sideDirectionMod = Random.Range(-1, 2) < 0 ? -1: 1 ;
    }
}

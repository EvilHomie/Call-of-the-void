using UnityEngine;

public class ImpulseLaser : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shootSound;

    float nextTimeToFire = 0;
    float fireRate = 5;

    readonly string[] fireButtonConditions = {"Pressed", "Ñlamped"};

    readonly float firePointRotateSpeed = 360f;
    readonly float weaponMaxAngle = 30f;
    readonly float damage = 100f;
    Vector3 targetPosition;


    void OnEnable()
    {
        PlayerControl.broadcastMousePosition += TakeTargetPosition;
        PlayerControl.broadcastStatusFiringButton += ShootingEvents;
    }

    void OnDisable()
    {
        PlayerControl.broadcastMousePosition -= TakeTargetPosition;
        PlayerControl.broadcastStatusFiringButton -= ShootingEvents;
    }

    void FixedUpdate()
    {
        RotateToTarget();
        
    }

    void ShootingEvents(string condition)
    {
        if (condition == fireButtonConditions[0])
        {
            StartShooting();
        }

        if (condition == fireButtonConditions[1]) ContinuedShooting();       
    }

    void StartShooting()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1/ fireRate;
            Vector3 offSet = firePoint.forward * 1.5f;
            Vector3 pos = firePoint.position + offSet;

            Instantiate(projectilePrefab, pos, firePoint.rotation);
            shootParticle.Play();
            audioSource.PlayOneShot(shootSound);
        }      
    }

    void ContinuedShooting()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Vector3 offSet = firePoint.forward * 1.5f;
            Vector3 pos = firePoint.position + offSet;

            Instantiate(projectilePrefab, pos, firePoint.rotation);
            shootParticle.Play();
            audioSource.PlayOneShot(shootSound);
        }
    }

    void RotateToTarget()
    {
        Vector3 targetDirection = targetPosition - firePoint.position;
        Vector3 targetDirection2D = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;
        Quaternion toTarget = Quaternion.LookRotation(targetDirection2D, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTarget, firePointRotateSpeed * Time.deltaTime);

        ConstraintRotation();
    }

    void ConstraintRotation()
    {
        if (transform.localEulerAngles.y > weaponMaxAngle & transform.localEulerAngles.y < 90f)
        {
            transform.localEulerAngles = new Vector3(0, weaponMaxAngle, 0);
        }
        if (transform.localEulerAngles.y < 360f - weaponMaxAngle & transform.localEulerAngles.y > 90f)
        {
            transform.localEulerAngles = new Vector3(0, -weaponMaxAngle, 0);
        }
    }

    void TakeTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }
}



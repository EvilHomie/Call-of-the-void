using UnityEngine;

public class ImpulseLaser : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] AudioSource audioSource;

    float nextTimeToFire = 0;
    float fireRate = 5;

    readonly string[] fireButtonConditions = {"Pressed", "Ñlamped"};

    readonly float firePointRotateSpeed = 360f;
    readonly float weaponMaxAngle = 30f;
    readonly float damage = 100f;
    Vector3 mousePosition;


    void OnEnable()
    {
        PlayerControl.broadcastMousePosition += TakeMousePosition;
        PlayerControl.broadcastStatusFiringButton += ShootingEvents;
    }

    void OnDisable()
    {
        PlayerControl.broadcastMousePosition -= TakeMousePosition;
        PlayerControl.broadcastStatusFiringButton -= ShootingEvents;
    }

    void FixedUpdate()
    {
        RotateToMouse();
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
            audioSource.Play();
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
            audioSource.Play();
        }
    }

    void RotateToMouse()
    {
        Vector3 mouseDirection = mousePosition - firePoint.position;
        Vector3 mouseDirection2D = new Vector3(mouseDirection.x, 0, mouseDirection.z).normalized;
        Quaternion toMouse = Quaternion.LookRotation(mouseDirection2D, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, firePointRotateSpeed * Time.deltaTime);

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

    void TakeMousePosition(Vector3 mousePos)
    {
        mousePosition = mousePos;
    }
}



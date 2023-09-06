using UnityEngine;

public class TurretImpulseLaser : MonoBehaviour
{

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] AudioSource audioSource;

    float nextTimeToFire;
    [SerializeField] float fireRate;



    [SerializeField] float turretShootDistance;
    [SerializeField] float offSetMyltipler;
    public bool targetInFocus;


    private void FixedUpdate()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit raycastHit, turretShootDistance))
        {
            if (raycastHit.collider.gameObject.CompareTag("Player"))
            {
                Shooting();
                targetInFocus = true;
            }
            else { targetInFocus = false; }
        }
        else { targetInFocus = false; }
    }
    void Shooting()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Vector3 offSet = firePoint.forward * offSetMyltipler;
            Vector3 pos = firePoint.position + offSet;

            Instantiate(projectilePrefab, pos, firePoint.rotation);
            shootParticle.Play();
            audioSource.Play();
        }
    }
}

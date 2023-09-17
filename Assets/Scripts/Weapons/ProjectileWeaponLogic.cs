using UnityEngine;

public class ProjectileWeaponLogic : MonoBehaviour, IWeapon
{

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectileObject;
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shootSound;

    float nextTimeToFire = 0;
    [SerializeField] float fireRate;
    [SerializeField] float energyDamage;
    [SerializeField] float kineticDamage;
    [SerializeField] float offSetSpawnProjectile;

    void Start()
    {
        SetProjectileDMG();
    }

    void SetProjectileDMG()
    {
        projectileObject.GetComponent<ProjectileManager>().energyDamage = energyDamage;
        projectileObject.GetComponent<ProjectileManager>().kineticDamage = kineticDamage;
    }
    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Vector3 offSet = firePoint.forward * offSetSpawnProjectile;
            Vector3 pos = firePoint.position + offSet;

            GameObject projectile = Instantiate(projectileObject, pos, firePoint.rotation);
            projectile.SetActive(true);
            shootParticle.Play();
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void Stop()
    {
    }

    public void SetParameters(float energyDmg, float kineticDmg)
    {
        energyDamage = energyDmg;
        kineticDamage = kineticDmg;
    }
}

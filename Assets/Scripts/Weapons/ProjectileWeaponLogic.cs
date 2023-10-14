using UnityEngine;

public class ProjectileWeaponLogic : MonoBehaviour, IWeapon
{

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePF;
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shootSound;

    float nextTimeToFire = 0;
    [SerializeField] float fireRate;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;
    [SerializeField] float energyDamage;
    [SerializeField] float kineticDamage;
    [SerializeField] float offSetSpawnProjectile;

    void Start()
    {
        projectilePF.GetComponent<ProjectileManager>().SetProjectileParameters(projectileSpeed, projectileLifeTime, energyDamage, kineticDamage);
    }

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Vector3 offSet = firePoint.forward * offSetSpawnProjectile;
            Vector3 pos = firePoint.position + offSet;

            GameObject projectile = Instantiate(projectilePF, pos, firePoint.rotation);
            projectile.SetActive(true);
            shootParticle.Play();
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void Stop()
    {
    }

    public void SetWeaponParameters(float fireRate, float projectileSpeed, float projectileLifeTime, float energyDMG, float kineticDMG, float beamLength)
    {
        this.fireRate = fireRate;
        this.projectileSpeed = projectileSpeed;
        this.projectileLifeTime = projectileLifeTime;
        energyDamage = energyDMG;
        kineticDamage = kineticDMG;
    }
}

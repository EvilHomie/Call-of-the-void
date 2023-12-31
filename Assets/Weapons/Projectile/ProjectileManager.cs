using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;
    [SerializeField] float energyDamage;
    [SerializeField] float kineticDamage;
    [SerializeField] float asteroidMultiplier;
    [SerializeField] float enemyMultiplier;
    [SerializeField] ParticleSystem hitEfect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward *  projectileSpeed, ForceMode.VelocityChange);

        Destroy(gameObject, projectileLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDadamageable target = collision.transform.root.gameObject.GetComponent<IDadamageable>();
        target?.Damage(energyDamage, kineticDamage, asteroidMultiplier, enemyMultiplier);

        Instantiate(hitEfect, collision.contacts[0].point, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDadamageable target = other.transform.root.gameObject.GetComponent<IDadamageable>();
        target?.Damage(energyDamage, kineticDamage, asteroidMultiplier, enemyMultiplier);
    }

    public void SetProjectileParameters(float speed, float lifeTime, float energyDMG, float kineticDMG, float asteroidMultiplier, float enemyMultiplier)
    {
        projectileSpeed = speed;
        projectileLifeTime = lifeTime;
        energyDamage = energyDMG;
        kineticDamage = kineticDMG;
        this.asteroidMultiplier = asteroidMultiplier;
        this.enemyMultiplier = enemyMultiplier;
    }
}

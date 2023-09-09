using UnityEngine;

public class TurretProjectileManager : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;
    public float energyDamage;
    public float kineticDamage;
    [SerializeField] ParticleSystem hitEfect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward *  projectileSpeed, ForceMode.VelocityChange);

        Destroy(gameObject, projectileLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDadamageable target = collision.gameObject.GetComponent<IDadamageable>();

        target?.Damage(energyDamage, kineticDamage);
        Instantiate(hitEfect, collision.contacts[0].point, transform.rotation * Quaternion.Euler(0f, 180f, 0f));

        Destroy(gameObject);
    }
}

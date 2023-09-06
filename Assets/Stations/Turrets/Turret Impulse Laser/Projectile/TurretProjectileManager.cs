using UnityEngine;

public class TurretProjectileManager : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 100;
    [SerializeField] float lifeTime = 2;
    public float turretDamage;
    [SerializeField] ParticleSystem hitEfect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward *  speed, ForceMode.VelocityChange);

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDadamageable target = collision.gameObject.GetComponent<IDadamageable>();

        target?.Damage(turretDamage);
        Instantiate(hitEfect, collision.contacts[0].point, transform.rotation * Quaternion.Euler(0f, 180f, 0f));

        Destroy(gameObject);
    }
}

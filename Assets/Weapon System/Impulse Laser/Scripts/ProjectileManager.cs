using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Rigidbody rb;
    float speed = 100;
    float lifeTime = 2;
    float damage = 25;
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

        target?.Damage(damage);
        Instantiate(hitEfect, collision.contacts[0].point, transform.rotation * Quaternion.Euler(0f, 180f, 0f));

        Destroy(gameObject);
    }
}

using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    IDadamageable IDadamageable;

    readonly float damageMultipler = 20;

    private void Awake()
    {
        IDadamageable = GetComponent<IDadamageable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionImpulse = collision.impulse;
        DamageCalculating(collisionImpulse.magnitude);
    }

    void DamageCalculating(float colForce)
    {
        float damageValue = colForce / damageMultipler;
        IDadamageable.Damage(damageValue, damageValue);
    }
}

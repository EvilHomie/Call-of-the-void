using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    IDadamageable IDadamageable;
        
    float damageMod = 10;

    private void Awake()
    {
        IDadamageable = GetComponent<IDadamageable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.GetComponent<ProjectileManager>())
        {
            Vector3 collisionImpulse = collision.impulse;
            DamageCalculating(collisionImpulse.magnitude);
        }        
    }

    void DamageCalculating(float colForce)
    {
        float damageValue = colForce / damageMod;
        IDadamageable.Damage(damageValue, damageValue, 1, 1);
    }
}

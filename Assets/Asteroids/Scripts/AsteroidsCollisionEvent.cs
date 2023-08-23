using UnityEngine;

public class AsteroidsCollisionEvent : MonoBehaviour
{
    AsteroidParameters asteroidParameters;

    float damageValue;
    Vector3 collisionForce;
    readonly float resistToCollision = 10000f;
    readonly float damageMultipler = 10;

    private void Awake()
    {
        asteroidParameters = GetComponent<AsteroidParameters>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionForce = collision.impulse / Time.fixedDeltaTime;
        
        DamageCalculating(collisionForce.magnitude);        
    }        

    void DamageCalculating(float colForce)
    {
        damageValue = colForce/resistToCollision * damageMultipler;
        asteroidParameters.TakeDamage(damageValue);
    }
}

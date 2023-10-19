using UnityEngine;

public class HitResLogic : MonoBehaviour, IDadamageable
{
    [SerializeField] ParticleSystem explosionParticle;
    bool isResDestroy = false;
    public void Damage(float energyDMG, float kineticDMG)
    {
        if (!isResDestroy)
        {
            isResDestroy = true;
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }        
    }    
}

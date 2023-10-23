using UnityEngine;

public class HitResLogic : MonoBehaviour, IDadamageable
{
    [SerializeField] ParticleSystem explosionParticle;
    bool isResDestroy = false;
    public void Damage(float energyDMG, float kineticDMG, float asteroidMultiplier, float enemyMultiplier)
    {
        if (!isResDestroy)
        {
            isResDestroy = true;
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }        
    }
    public void SetMaxHpParameters(float hullHP, float armorHP, float shieldHP)
    {
    }

    public void SetRegRates(float hullRegRate, float armorRegRate, float shieldRegRate, float shieldStartRegDelay)
    {
    }
}

using UnityEngine;

public class AsteroidParameters : MonoBehaviour, IDadamageable
{
    [SerializeField] float asteroidHP;
    [SerializeField] GameObject blowDownParticle;

    private void Awake()
    {
        asteroidHP = GetComponent<Rigidbody>().mass;
    }

    public void Damage(float damageValue)
    {
        asteroidHP -= damageValue;

        if (asteroidHP < 0)
        {
            Instantiate(blowDownParticle, transform.position, blowDownParticle.transform.rotation);
            Destroy(gameObject);            
        }
    }
}

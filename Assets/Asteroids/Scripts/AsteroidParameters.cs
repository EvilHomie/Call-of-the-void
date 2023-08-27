using UnityEngine;

public class AsteroidParameters : MonoBehaviour, IDadamageable
{
    [SerializeField] GameObject explosionParticle;
    [SerializeField] float asteroidHP;    

    private void Awake()
    {
        asteroidHP = GetComponent<Rigidbody>().mass;
    }

    public void Damage(float damageValue)
    {
        asteroidHP -= damageValue;

        if (asteroidHP < 0)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);            
        }
    }
}

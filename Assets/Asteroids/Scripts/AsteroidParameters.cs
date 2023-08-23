using UnityEngine;

public class AsteroidParameters : MonoBehaviour
{
    float asteroidHP;

    private void Awake()
    {
        asteroidHP = GetComponent<Rigidbody>().mass;
    }

    public void TakeDamage(float damage)
    {
        asteroidHP -= damage;

        if (asteroidHP < 0) { Destroy(gameObject); }
    }
}

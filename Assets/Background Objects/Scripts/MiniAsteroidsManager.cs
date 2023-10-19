using UnityEngine;

public class MiniAsteroidsManager : MonoBehaviour
{
    ParticleSystem _particleSystem;
    ParticleSystem.EmissionModule emission;

    [SerializeField] float defRateOverTime;
    [SerializeField] float defRateOverDistance;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        emission = _particleSystem.emission;
    }
    private void OnEnable()
    {        
        SetDefValues();
        EventBus.onPlayerInAsteroidField += ChangeEmissionRate;
    }

    private void OnDisable()
    {
        EventBus.onPlayerInAsteroidField -= ChangeEmissionRate;
    }

    void SetDefValues()
    {        
        emission.rateOverTime = defRateOverTime;
        emission.rateOverDistance = defRateOverDistance;
    }

    void ChangeEmissionRate(float multipler)
    {
        emission.rateOverTime = defRateOverTime * multipler;
        emission.rateOverDistance = defRateOverDistance * multipler;
    }    
}

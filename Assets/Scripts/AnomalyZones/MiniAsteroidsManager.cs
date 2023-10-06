using Unity.VisualScripting;
using UnityEngine;

public class MiniAsteroidsManager : MonoBehaviour
{
    ParticleSystem _particleSystem;
    ParticleSystem.EmissionModule emission;

    [SerializeField] float defRateOverTime = 0.2f;
    [SerializeField] float defRateOverDistance = 0.1f;


    private void OnEnable()
    {        
        GetDefValues();
        PlayerInFiledManager.comandToAsteroids += ChangeEmissionRate;
    }

    private void OnDisable()
    {
        PlayerInFiledManager.comandToAsteroids -= ChangeEmissionRate;
    }

    void GetDefValues()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        emission = _particleSystem.emission;
        emission.rateOverTime = defRateOverTime;
        emission.rateOverDistance = defRateOverDistance;
    }

    void ChangeEmissionRate(float multipler)
    {
        emission.rateOverTime = defRateOverTime * multipler;
        emission.rateOverDistance = defRateOverDistance * multipler;
        defRateOverTime *= multipler;
        defRateOverDistance *= multipler;
    }    
}

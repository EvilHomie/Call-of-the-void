using UniRx;
using UnityEngine;

public class MiniAsteroidsManager : MonoBehaviour
{
    CompositeDisposable _disposables = new();
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
        EventBus.AsteroidsSpawnMod.Subscribe(mod => ChangeEmissionRate(mod)).AddTo(_disposables);
    }

    private void OnDisable()
    {
        _disposables.Clear();
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

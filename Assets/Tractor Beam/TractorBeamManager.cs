using UniRx;
using UnityEngine;

public class TractorBeamManager : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        EventBus.TractorBeamActiveStatus.Subscribe(status => TractorBeamSoundManager(status)).AddTo(_disposable);
    }

    void OnDisable()
    {
        _disposable.Clear();
        audioSource.Stop();
    }

    void TractorBeamSoundManager(bool status)
    {
        if (status) audioSource.Play();
        else audioSource.Stop();
    }
}

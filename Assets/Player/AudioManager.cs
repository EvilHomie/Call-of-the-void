using UniRx;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    CompositeDisposable _disposables = new();

    AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventBus.CommandForPlaySound.Subscribe(sound => PlaySound(sound)).AddTo(_disposables);
    }

    private void OnDisable()
    {
        _disposables.Clear();
    }

    void PlaySound(AudioClip sound)
    {
        _source.PlayOneShot(sound);
    }
}

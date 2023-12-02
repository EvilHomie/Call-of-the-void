using UniRx;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    CompositeDisposable _disposables = new();

    AudioSource _source;

    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip errorSound;

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

    void PlaySound(string clipName)
    {
        if (clipName == "successSound")
            _source.PlayOneShot(successSound);
        else if (clipName == "errorSound")
            _source.PlayOneShot(errorSound);
    }
}

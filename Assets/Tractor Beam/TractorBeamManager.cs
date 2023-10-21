using UniRx;
using UnityEngine;

public class TractorBeamManager : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    AudioSource audioSource;
    [SerializeField] AudioClip resPickUpSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        EventBus.ComandOnCollectResource.Subscribe(res => PlayPickUpSound()).AddTo(_disposable);

        EventBus.TractorBeamActiveStatus.Subscribe(status => TractorBeamSoundManager(status)).AddTo(_disposable);
    }

    void OnDisable()
    {
        _disposable.Clear();
        audioSource.Stop();
    }

    void PlayPickUpSound()
    {
        audioSource.PlayOneShot(resPickUpSound);
    }

    void TractorBeamSoundManager(bool status)
    {
        if (status) audioSource.Play();
        else audioSource.Stop();
    }


}

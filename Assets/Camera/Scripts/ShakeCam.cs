using Cinemachine;
using System.Collections;
using UniRx;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin perlinNoise;

    float amplitude = 1;
    float frequency = 1;
    float duration = 1;
    bool cameraIsShaking = false;


    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        EventBus.ComandOnPlayerTakeDamage.Subscribe(_ => ShakeCamera()).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    void ShakeCamera()
    {
        if (!cameraIsShaking)
        {
            perlinNoise.m_AmplitudeGain = amplitude;
            perlinNoise.m_FrequencyGain = frequency;
            StartCoroutine(ShakeCameraTurnOff(duration));

            IEnumerator ShakeCameraTurnOff(float duration)
            {
                cameraIsShaking = true;
                yield return new WaitForSeconds(duration);
                perlinNoise.m_AmplitudeGain = 0;
                perlinNoise.m_FrequencyGain = 0;
                cameraIsShaking = false;
            }
        }        
    }
}

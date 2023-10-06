using Cinemachine;
using System.Collections;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin perlinNoise;

    float amplitude = 1;
    float frequency = 1;
    float duration = 1;
    bool cameraIsShaking = false;


    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        EventBus.shakeCam += ShakeCamera;
    }

    private void OnDisable()
    {
        EventBus.shakeCam -= ShakeCamera;
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

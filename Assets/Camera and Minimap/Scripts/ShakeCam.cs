using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin perlinNoise;

    float amplitude = 2;
    float frequency = 2;
    float duration = 1;


    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        PlayerShipParameters.onTakeDamageEvents += ShakeCameraTurnOn;
    }

    private void OnDisable()
    {
        PlayerShipParameters.onTakeDamageEvents -= ShakeCameraTurnOn;
    }

    void ShakeCameraTurnOn()
    {
        perlinNoise.m_AmplitudeGain = amplitude;
        perlinNoise.m_FrequencyGain = frequency;
        StartCoroutine(ShakeCameraTurnOff(duration));

        IEnumerator ShakeCameraTurnOff(float duration)
        {
            yield return new WaitForSeconds(duration);
            perlinNoise.m_AmplitudeGain = 0;
            perlinNoise.m_FrequencyGain = 0;
        }
    }  

    
}

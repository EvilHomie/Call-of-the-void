using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin perlinNoise;

    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void ShakeCamera(float amplitude, float frequency, float duration)
    {
        perlinNoise.m_AmplitudeGain = amplitude;
        perlinNoise.m_FrequencyGain = frequency;
        StartCoroutine(Duration(duration));

        IEnumerator Duration(float duration)
        {
            yield return new WaitForSeconds(duration);
            perlinNoise.m_AmplitudeGain = 0;
            perlinNoise.m_FrequencyGain = 0;
        }
    }  

    private void OnEnable()
    {
        PlayerShipParameters.onTakeDamage += ShakeCamera;
    }

    private void OnDisable()
    {
        PlayerShipParameters.onTakeDamage -= ShakeCamera;
    }
}

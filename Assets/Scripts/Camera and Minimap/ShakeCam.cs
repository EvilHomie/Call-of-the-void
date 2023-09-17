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


    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        EventsPlayerOnCollisions.collisionEvent += ShakeCameraTurnOn;
    }

    private void OnDisable()
    {
        EventsPlayerOnCollisions.collisionEvent -= ShakeCameraTurnOn;
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

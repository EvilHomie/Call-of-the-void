using System;
using UnityEngine;

public class AsteriodFieldManager : MonoBehaviour
{
    [SerializeField] float countMultipler;
    [SerializeField] float triggerDistanceFromPlayer;
    [SerializeField] bool playerIsInsideField = false;

    public static Action<float> comandToAsteroids;
    float currentDistanceFromPlayer;

    Vector3 playePos;
    private void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += GetPlayerPos;
        InvokeRepeating(nameof(CheckDistance), 5, 2);
    }
    void GetPlayerPos(Transform playerTransform)
    {
        playePos = playerTransform.position;
    }
    void CheckDistance()
    {
        currentDistanceFromPlayer = Vector3.Distance(playePos, transform.position);

        if (!playerIsInsideField)
        {            
            if (currentDistanceFromPlayer < triggerDistanceFromPlayer)
            {
                playerIsInsideField = true;
                comandToAsteroids?.Invoke(countMultipler);
            }
        }
        else
        {
            if (currentDistanceFromPlayer > triggerDistanceFromPlayer)
            {
                playerIsInsideField = false;
                comandToAsteroids?.Invoke(1 / countMultipler);
            }
        }
    }
}

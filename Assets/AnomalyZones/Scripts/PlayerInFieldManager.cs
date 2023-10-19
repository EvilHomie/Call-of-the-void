using System;
using UnityEngine;

public class PlayerInFieldManager : MonoBehaviour
{
    [SerializeField] float countMultipler;
    [SerializeField] float speedBost;
    [SerializeField] float triggerDistanceFromPlayer;
    [SerializeField] bool playerIsInsideField = false;

    
    float currentDistanceFromPlayer;

    private void Awake()
    {
        InvokeRepeating(nameof(CheckDistance), 5, 1);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }

    void CheckDistance()
    {
        currentDistanceFromPlayer = Vector3.Distance(GlobalData.playerTransform.position, transform.position);

        if (!playerIsInsideField)
        {            
            if (currentDistanceFromPlayer < triggerDistanceFromPlayer)
            {
                playerIsInsideField = true;
                EventBus.onPlayerInAsteroidField?.Invoke(countMultipler);
            }
        }
        else
        {
            if (currentDistanceFromPlayer > triggerDistanceFromPlayer)
            {
                playerIsInsideField = false;
                EventBus.onPlayerInAsteroidField?.Invoke(1);
            }
        }
    }
}

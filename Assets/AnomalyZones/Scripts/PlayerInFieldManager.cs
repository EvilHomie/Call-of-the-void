using System;
using UnityEngine;

public class PlayerInFieldManager : MonoBehaviour
{
    [SerializeField] float countMultipler;
    [SerializeField] float speedBost;
    [SerializeField] float triggerDistanceFromPlayer;
    [SerializeField] bool playerIsInsideField = false;

    
    float currentDistanceFromPlayer;

    private void OnEnable()
    {
        InvokeRepeating(nameof(CheckDistance), 5, 1);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    void CheckDistance()
    {
        currentDistanceFromPlayer = Vector3.Distance(GlobalData.PlayerTransform.position, transform.position);

        if (!playerIsInsideField)
        {            
            if (currentDistanceFromPlayer < triggerDistanceFromPlayer)
            {
                playerIsInsideField = true;
                EventBus.AsteroidsSpawnMod.Value = countMultipler;
            }
        }
        else
        {
            if (currentDistanceFromPlayer > triggerDistanceFromPlayer)
            {
                playerIsInsideField = false;
                EventBus.AsteroidsSpawnMod.Value = 1;
            }
        }
    }
}

using UnityEngine;

public class DestroyIfExceededDistanceFromPlayer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromPlayer;
    readonly float delayCheckDistance = 2;

    private void Awake()
    {
        InvokeRepeating(nameof(CheckDistance), delayCheckDistance, delayCheckDistance);
    }
    
    void CheckDistance()
    {
        float curentDistanceFromPlayer = Vector3.Distance(transform.position, GlobalData.playerTransform.position);
        if (curentDistanceFromPlayer > maxDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class DestroyIfExceededDistanceFromPlayer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromPlayer;
    readonly float delayCheckDistance = 2;

    private void OnEnable()
    {
        InvokeRepeating(nameof(CheckDistance), delayCheckDistance, delayCheckDistance);
    }
    private void OnDisable()
    {
        CancelInvoke();
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

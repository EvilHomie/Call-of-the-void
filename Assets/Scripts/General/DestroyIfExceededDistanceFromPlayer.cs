using UnityEngine;

public class DestroyIfExceededDistanceFromPlayer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromPlayer;
    readonly float delayCheckDistance = 2;
    Vector3 playerPos;

    private void OnEnable()
    {
        EventBus.broadcastPlayerTransform += GetPlayerPos;
        InvokeRepeating(nameof(CheckDistance), delayCheckDistance, delayCheckDistance);
    }
    private void OnDisable()
    {
        EventBus.broadcastPlayerTransform -= GetPlayerPos;
    }

    void GetPlayerPos(Transform playerTransform)
    {
        playerPos = playerTransform.position;
    }

    void CheckDistance()
    {
        float curentDistanceFromPlayer = Vector3.Distance(transform.position, playerPos);
        if (curentDistanceFromPlayer > maxDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }
}

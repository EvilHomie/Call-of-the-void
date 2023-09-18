using UnityEngine;

public class CheckDistanceToPlayer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromPlayer;
    readonly float delayCheckDistance = 2;
    Vector3 playerPos;

    private void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += GetPlayerPos;
        InvokeRepeating(nameof(CheckDistance), delayCheckDistance, delayCheckDistance);
    }
    private void OnDisable()
    {
        PlayerControl.broadcastPlayerTransform -= GetPlayerPos;
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

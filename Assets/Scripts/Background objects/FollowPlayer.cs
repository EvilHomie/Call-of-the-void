using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private void OnEnable()
    {
        EventBus.broadcastPlayerTransform += SetPosition;
    }
    private void OnDisable()
    {
        EventBus.broadcastPlayerTransform -= SetPosition;
    }

    private void SetPosition(Transform playerTransform)
    {
        transform.position = playerTransform.position + offset;
    }
}

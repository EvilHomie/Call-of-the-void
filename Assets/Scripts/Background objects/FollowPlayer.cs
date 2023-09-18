using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += SetPosition;
    }
    private void OnDisable()
    {
        PlayerControl.broadcastPlayerTransform -= SetPosition;
    }

    private void SetPosition(Transform playerTransform)
    {
        transform.position = playerTransform.position + offset;
    }
}

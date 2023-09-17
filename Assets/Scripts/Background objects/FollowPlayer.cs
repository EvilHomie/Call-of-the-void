using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private void OnEnable()
    {
        PlayerControl.broadcastPlayerPosition += SetPosition;
    }
    private void OnDisable()
    {
        PlayerControl.broadcastPlayerPosition -= SetPosition;
    }

    private void SetPosition(Vector3 playerPos)
    {
        transform.position = playerPos + offset;
    }
}

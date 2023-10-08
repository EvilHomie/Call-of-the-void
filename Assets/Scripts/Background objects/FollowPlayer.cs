using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        transform.position = GlobalData.playerTransform.position + offset;
    }
}

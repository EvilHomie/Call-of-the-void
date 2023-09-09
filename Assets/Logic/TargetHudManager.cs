using UnityEngine;

public class TargetHudManager : MonoBehaviour
{
    private void OnMouseDown()
    {
        SetActiveTarget();
    }

    void SetActiveTarget()
    {
        if (GetComponent<BroadcastTargetParameters>() == null)
        {
            BroadcastTargetParameters lastTarget = FindFirstObjectByType<BroadcastTargetParameters>();
            if (lastTarget != null)
            {
                Destroy(lastTarget);
            }
            gameObject.AddComponent<BroadcastTargetParameters>();
        }
    }
}

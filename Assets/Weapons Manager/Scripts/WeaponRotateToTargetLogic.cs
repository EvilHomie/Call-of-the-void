using UnityEngine;

public class WeaponRotateToTargetLogic : MonoBehaviour
{
    [SerializeField] Transform pivotPoint;


    [SerializeField] float weaponRotateSpeed;
    [SerializeField] float weaponMaxAngle;
    Vector3 targetPosition;


    void OnEnable()
    {
        if (!transform.root.CompareTag("Player"))
        {
            PlayerControl.broadcastPlayerPosition += TakeTargetPosition;
        }
        else { PlayerControl.broadcastMousePosition += TakeTargetPosition; }
    }

    void OnDisable()
    {
        if (!transform.root.CompareTag("Player"))
        {
            PlayerControl.broadcastPlayerPosition -= TakeTargetPosition;
        }
        else { PlayerControl.broadcastMousePosition -= TakeTargetPosition; }
    }


    private void FixedUpdate()
    {
        RotateToTarget();
    }
    void RotateToTarget()
    {
        Vector3 targetDirection = targetPosition - pivotPoint.position;
        Vector3 targetDirection2D = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;
        Quaternion toTarget = Quaternion.LookRotation(targetDirection2D, Vector3.up);
        pivotPoint.rotation = Quaternion.RotateTowards(pivotPoint.rotation, toTarget, weaponRotateSpeed * Time.fixedDeltaTime);

        ConstraintRotation();
    }

    void ConstraintRotation()
    {
        if (pivotPoint.localEulerAngles.y > weaponMaxAngle & pivotPoint.localEulerAngles.y < 90f)
        {
            pivotPoint.localEulerAngles = new Vector3(0, weaponMaxAngle, 0);
        }
        if (pivotPoint.localEulerAngles.y < 360f - weaponMaxAngle & pivotPoint.localEulerAngles.y > 90f)
        {
            pivotPoint.localEulerAngles = new Vector3(0, -weaponMaxAngle, 0);
        }
    }

    void TakeTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }
}

using UnityEngine;

public class TurretTrackTarget : MonoBehaviour
{
    [SerializeField] Transform pivotPoint;


    readonly float turretRotateSpeed = 10f;
    readonly float weaponMaxAngle = 75f;
    Vector3 targetPosition;


    void OnEnable()
    {
        PlayerControl.broadcastPlayerPosition += TakeTargetPosition;
    }

    void OnDisable()
    {
        PlayerControl.broadcastPlayerPosition -= TakeTargetPosition;
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
        pivotPoint.rotation = Quaternion.RotateTowards(pivotPoint.rotation, toTarget, turretRotateSpeed * Time.fixedDeltaTime);

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

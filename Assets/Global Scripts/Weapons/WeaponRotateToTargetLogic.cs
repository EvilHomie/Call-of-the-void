using System;
using UnityEngine;

public class WeaponRotateToTargetLogic : MonoBehaviour
{
    [SerializeField] Transform pivotPoint;


    [SerializeField] float weaponRotateSpeed;
    [SerializeField] float weaponMaxAngle;
    Vector3 targetPosition;

    bool parentIsPlayer;


    void Awake()
    {
        if (transform.root.CompareTag("Player"))
            parentIsPlayer = true;
        else parentIsPlayer = false;
    }

    private void FixedUpdate()
    {
        if (parentIsPlayer) targetPosition = GlobalData.mousePos;
        else targetPosition = GlobalData.playerTransform.position;
        
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
}

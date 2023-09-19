using System.Collections.Generic;
using UnityEngine;

public class StationRotateToTarget : MonoBehaviour
{

    readonly float stationRotateSpeed = 5f;
    Vector3 targetPosition;
    ShootIfPlayerInFocus[] turretsArray;
    List<bool> targetInFocus = new();

    private void Awake()
    {
        turretsArray = GetComponentsInChildren<ShootIfPlayerInFocus>();
        FillList();
    }
    void OnEnable()
    {
        PlayerControl.broadcastPlayerTransform += TakeTargetPosition;
    }

    void OnDisable()
    {
        PlayerControl.broadcastPlayerTransform -= TakeTargetPosition;
    }


    private void FixedUpdate()
    {
        if (!CheckIfTargetInFocus())
        {
            RotateToTarget();
        }
    }
    void RotateToTarget()
    {
        Vector3 targetDirection = targetPosition - transform.position;
        Vector3 targetDirection2D = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;
        Quaternion toTarget = Quaternion.LookRotation(targetDirection2D, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTarget, stationRotateSpeed * Time.fixedDeltaTime);

    }

    void TakeTargetPosition(Transform playerTransform)
    {
        targetPosition = playerTransform.position;
    }

    void FillList()
    {
        targetInFocus.Clear();
        foreach (var t in turretsArray) { targetInFocus.Add(t.targetInFocus); }
    }

    bool CheckIfTargetInFocus()
    {
        FillList();
        if (targetInFocus.Exists(x => x == true)) { return true; }
        else { return false; }
    }
}



//Transform turretWithFocusOnTarget;
//int indexTurretWithFocus = targetInFocus.IndexOf(true);
//turretWithFocusOnTarget = turretsList[indexTurretWithFocus].gameObject.transform;
//Debug.Log(turretWithFocusOnTarget.name);

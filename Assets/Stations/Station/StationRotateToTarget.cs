using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class StationRotateToTarget : MonoBehaviour
{

    readonly float stationRotateSpeed = 5f;
    Vector3 targetPosition;
    TurretImpulseLaser[] turretsList;
    List<bool> targetInFocus = new();

    private void Awake()
    {
        turretsList = GetComponentsInChildren<TurretImpulseLaser>();
        FillList();
    }
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

    void TakeTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }

    void FillList()
    {
        targetInFocus.Clear();
        foreach (var t in turretsList) { targetInFocus.Add(t.targetInFocus); }
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

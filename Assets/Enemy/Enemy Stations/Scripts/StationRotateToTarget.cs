using System.Collections.Generic;
using UnityEngine;

public class StationRotateToTarget : MonoBehaviour
{

    readonly float stationRotateSpeed = 5f;
    EnemyWeaponShootInPlayerLogic[] turretsArray;
    List<bool> targetInFocus = new();

    private void Awake()
    {
        turretsArray = GetComponentsInChildren<EnemyWeaponShootInPlayerLogic>();
        FillList();
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
        Vector3 targetDirection = GlobalData.PlayerTransform.position - transform.position;
        Vector3 targetDirection2D = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;
        Quaternion toTarget = Quaternion.LookRotation(targetDirection2D, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTarget, stationRotateSpeed * Time.fixedDeltaTime);
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

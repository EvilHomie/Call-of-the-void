using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnomalyDirectionManager : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    GameObject player;

    Vector3 anomalyDir;
    Vector3 anomalyPos;
    float angle;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyStationSpawning.broadcastStationPosition += RotateToAnomaly;
    }

    private void OnDisable()
    {
        EnemyStationSpawning.broadcastStationPosition -= RotateToAnomaly;
    }

    private void FixedUpdate()
    {


        angle = Vector3.Angle(Vector3.forward, anomalyPos - player.transform.position);

        Debug.Log(angle);
        Debug.DrawRay(player.transform.position, anomalyPos - player.transform.position, Color.green);
        Debug.DrawRay(player.transform.position, Vector3.forward * 100, Color.green);
        RotateToAnomaly();
    }

    void RotateToAnomaly(Vector3 anomalyPos)
    {
        this.anomalyPos = anomalyPos;
        anomalyDir = transform.position - anomalyPos;
    }

    void RotateToAnomaly()
    {
        Vector3 dir = anomalyDir - transform.position;
        Quaternion toTarget = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTarget, 360);
    }
}

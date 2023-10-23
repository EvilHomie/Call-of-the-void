using UnityEngine;

public class ResMoveOnSpawn : MonoBehaviour
{
    Rigidbody resRB;

    [SerializeField] float moveSpeed;
    [SerializeField] float minMoveSpeed = 10;
    [SerializeField] float maxMoveSpeed = 15;

    [SerializeField] float tumbleSpeed;
    [SerializeField] float minTumbleSpeed = 10;
    [SerializeField] float maxTumbleSpeed = 15;

    private void Start()
    {
        resRB = GetComponent<Rigidbody>();
        RandomRotator();
        Move();
    }


    void RandomRotator()
    {
        tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed);
        resRB.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
    }


    void Move()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        Vector3 dir = new(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y);
        resRB.AddForce(dir * moveSpeed, ForceMode.VelocityChange);
    }
}

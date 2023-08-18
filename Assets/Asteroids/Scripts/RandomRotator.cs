using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    float tumbleSpeed;
    readonly float minTumbleSpeed = 0.25f;
    readonly float maxTumbleSpeed = 1.0f;

    void Awake()
    {
        tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumbleSpeed;
    }
}
using System;
using UnityEngine;

public class EventsPlayerOnCollisions : MonoBehaviour
{
    public static Action collisionEvent;

    private void OnCollisionEnter(Collision collision)
    {
        collisionEvent?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset = new(0, 150, 0);
    void Update()
    {
        transform.position = player.transform.position - offset;
        ;
    }
}

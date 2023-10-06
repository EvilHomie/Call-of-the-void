using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerTransform;
    private void OnEnable()
    {
        EventBus.broadcastPlayerTransform += SetPlayerPos;
    }

    private void OnDisable()
    {
        EventBus.broadcastPlayerTransform -= SetPlayerPos;
    }

    private void SetPlayerPos(Transform transform)
    {
        playerTransform = transform;
    }
}

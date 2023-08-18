using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGPlayerFollow : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)player.transform.position;
    }
}

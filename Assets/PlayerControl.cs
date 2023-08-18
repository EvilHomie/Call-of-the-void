using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;

    float playerSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }



    void PlayerController()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.AddForce(horizontalInput * playerSpeed * Vector3.right);
        rb.AddForce(verticalInput * playerSpeed * Vector3.forward);        
    }
}

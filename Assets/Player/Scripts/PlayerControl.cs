using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    public Camera mainCamera;
    public LayerMask layerMask;

    readonly float playerSpeed = 50f;
    readonly float playerRotateSpeed = 180f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        MousePosTrack();
        MoveToMouse();        
    }


    void MousePosTrack()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {    
            Vector3 direction = raycastHit.point - new Vector3(transform.position.x, 0, transform.position.z);
            Quaternion toMouse = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, playerRotateSpeed * Time.deltaTime);
        }
    }

    void MoveToMouse()
    {
        if (Input.GetMouseButton(1))
        {
            rb.AddForce( transform.forward * playerSpeed, ForceMode.Acceleration);
        }
    }        
}

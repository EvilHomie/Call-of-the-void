using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody playerRb;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;   

    Vector3 mousePosition2D;

    [SerializeField] float mainEngineSpeed;
    [SerializeField] float playerRotateSpeed;
    [SerializeField] float rCSSpeed;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();

        GlobalData.MousePos = mousePosition2D;
        GlobalData.PlayerTransform = transform;
        GlobalData.PlayerVelocity = playerRb.velocity;
    }

    
    void FixedUpdate()
    {
        MousePosTrack();
        MoveForward();
        AditionMove();

        GlobalData.MousePos = mousePosition2D;
        GlobalData.PlayerTransform = transform;
        GlobalData.PlayerVelocity = playerRb.velocity;
    }

    void MousePosTrack()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            RotateToMouse(raycastHit);
        }
    }

    void RotateToMouse(RaycastHit raycastHit)
    {
        Vector3 mousePosition = raycastHit.point;
        mousePosition2D = new Vector3(mousePosition.x, 0, mousePosition.z);
        Vector3 mouseDirection = mousePosition2D - transform.position;
        Quaternion toMouse = Quaternion.LookRotation(mouseDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, playerRotateSpeed * Time.deltaTime);
    }
    void MoveForward()
    {
        if (Input.GetButton("Fire2") && Time.timeScale == 1)
        {
            playerRb.AddForce(transform.forward * mainEngineSpeed, ForceMode.Acceleration);
        }    
    }

    void AditionMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(rCSSpeed * horizontalInput * transform.right, ForceMode.Acceleration);
        playerRb.AddForce(rCSSpeed * verticalInput * transform.forward, ForceMode.Acceleration);
    }
}

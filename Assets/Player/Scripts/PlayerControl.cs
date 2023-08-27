using System;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    // закоментированы участки кода имеющие отношение к имитации влияния отдалености планеты на скорость корабля в скрипте BigObjectManager
    Rigidbody playerRb;
    //WeaponManager weaponManager;    
    [SerializeField] BeamLaser beamLaser;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;    

    public static Action<Vector3> broadcastPlayerVelocity;
    public static Action<Vector3> broadcastMousePosition;
    public static Action<string> broadcastStatusFiringButton;

    Vector3 mouseDirection;
    Vector3 mousePosition;

    readonly float playerSpeed = 50f;
    readonly float playerRotateSpeed = 180f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //weaponManager = GetComponent<WeaponManager>();
    }

    
    void FixedUpdate()
    {
        MousePosTrack();
        MoveToMouse();

        broadcastMousePosition?.Invoke(mousePosition);
        broadcastPlayerVelocity?.Invoke(playerRb.velocity);
    }
    private void Update()
    {
        Shoot();
    }

    private void OnDisable()
    {
        if (Input.GetButton("Fire1")) broadcastStatusFiringButton?.Invoke("Released");
    }
    private void OnEnable()
    {
        if (Input.GetButton("Fire1")) broadcastStatusFiringButton?.Invoke("Pressed");
    }

    void MousePosTrack()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            mousePosition = raycastHit.point;
            mouseDirection = (mousePosition - new Vector3(transform.position.x, raycastHit.point.y, transform.position.z)).normalized;
            Quaternion toMouse = Quaternion.LookRotation(mouseDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, playerRotateSpeed * Time.deltaTime);
        }
    }

    void MoveToMouse()
    {
        if (Input.GetButton("Fire2"))
        {
            playerRb.AddForce( transform.forward * playerSpeed, ForceMode.Acceleration);            
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            broadcastStatusFiringButton?.Invoke("Pressed");
        }

        if (Input.GetButton("Fire1"))
        {
            broadcastStatusFiringButton?.Invoke("Сlamped");
        }

        if (Input.GetButtonUp("Fire1"))
        {
            broadcastStatusFiringButton?.Invoke("Released");
        }
    }
}

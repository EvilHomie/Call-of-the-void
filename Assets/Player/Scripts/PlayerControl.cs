using System;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    // закоментированы участки кода имеющие отношение к имитации влияния отдалености планеты на скорость корабля в скрипте BigObjectManager
    Rigidbody playerRb;
    //WeaponManager weaponManager;    
    //[SerializeField] BeamLaser beamLaser;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] ParticleSystem engineParticle;

    public static Action<Vector3> broadcastPlayerVelocity;
    public static Action<Vector3> broadcastMousePosition;
    public static Action<string> broadcastStatusFiringButton;

    public GameObject virCam;

    Vector3 mouseDirection;
    Vector3 mousePosition2D;

    readonly float mainEngineSpeed = 1000f;
    readonly float playerRotateSpeed = 180f;
    readonly float rCSSpeed = 500f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //weaponManager = GetComponent<WeaponManager>();
    }

    
    void FixedUpdate()
    {
        MousePosTrack();
        MoveForward();

        broadcastMousePosition?.Invoke(mousePosition2D);
        broadcastPlayerVelocity?.Invoke(playerRb.velocity);

    }

    private void Update()
    {
        Shoot();
        AditionControl();
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
            Vector3 mousePosition = raycastHit.point;
            mousePosition2D = new Vector3(mousePosition.x, 0 , mousePosition.z);            
            mouseDirection = mousePosition2D - transform.position ;
            Quaternion toMouse = Quaternion.LookRotation(mouseDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, playerRotateSpeed * Time.deltaTime);
        }
    }

    void MoveForward()
    {
        if (Input.GetButton("Fire2"))
        {
            playerRb.AddForce(mainEngineSpeed * Time.fixedDeltaTime * transform.forward, ForceMode.Acceleration);
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

    void AditionControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(rCSSpeed * horizontalInput * Time.deltaTime * transform.right, ForceMode.Acceleration);
        playerRb.AddForce(rCSSpeed * verticalInput * Time.deltaTime * transform.forward, ForceMode.Acceleration);
    }
}

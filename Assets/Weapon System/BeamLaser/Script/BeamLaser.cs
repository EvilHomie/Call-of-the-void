using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class BeamLaser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject startVFX;
    [SerializeField] GameObject endVFX;
    [SerializeField] AudioClip startShoot;
    [SerializeField] AudioClip endShoot;
    [SerializeField] AudioClip continueShoot;
    AudioSource audioSource;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    readonly float beamLength = 200f;
    float firePointRotateSpeed = 360f;
    float weaponMaxAngle = 30f;

    public Vector3 mousePosition;
    Vector3 mouseDirection2D;
    float damage =50;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Debug.Log(audioSource.gameObject.name);
        lineRenderer.enabled = false;
        PlayerControl.broadcastMousePosition += TakeMousePosition;
        FillListst();
        DisableLaser();
    }

    private void OnDisable()
    {
        PlayerControl.broadcastMousePosition -= TakeMousePosition;
    }
    public void EnableLaser()
    {
        lineRenderer.enabled = true;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }

        audioSource.PlayOneShot(startShoot);

    }

    public void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer. SetPosition(1, firePoint.position + firePoint.forward * beamLength);

        if (Physics.Raycast(transform.position, firePoint.forward, out RaycastHit raycastHit, float.MaxValue))
        {
            lineRenderer.SetPosition(1, raycastHit.point);
            IDadamageable hit = raycastHit.collider.GetComponent<IDadamageable>();
            if (hit != null )
            {
                hit.Damage(damage * Time.deltaTime);
            }
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);

    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
        audioSource.Stop();
        audioSource.PlayOneShot(endShoot, 0.4f);
    }

    private void FixedUpdate()
    {
        RotateToMouse();
    }

    public void RotateToMouse()
    {       
        Vector3 mouseDirection = mousePosition - firePoint.position;
        mouseDirection2D = new Vector3(mouseDirection.x, 0, mouseDirection.z).normalized;
        Quaternion toMouse = Quaternion.LookRotation(mouseDirection2D, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toMouse, firePointRotateSpeed * Time.deltaTime);

        ConstraintRotation();
    }

    void ConstraintRotation()
    {
        if (transform.localEulerAngles.y > weaponMaxAngle & transform.localEulerAngles.y < 90f)
        {
            transform.localEulerAngles = new Vector3(0, weaponMaxAngle, 0);
        }
        if (transform.localEulerAngles.y < 360f - weaponMaxAngle & transform.localEulerAngles.y > 90f)
        {
            transform.localEulerAngles = new Vector3(0, -weaponMaxAngle, 0);
        }
    }

    void TakeMousePosition(Vector3 mousePos)
    {
        mousePosition = mousePos;
    }

    void FillListst()
    {
        for (int i = 0; i<startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }

}


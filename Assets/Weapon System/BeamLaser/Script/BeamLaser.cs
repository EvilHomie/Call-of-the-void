using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLaser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject permanentStartPointParticlesContainer;
    [SerializeField] GameObject permanentEndPointParticlesContainer;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip startShoot;
    [SerializeField] AudioClip endShoot;
    [SerializeField] AudioClip continueShoot;    

    [SerializeField] ParticleSystem shootStartingParticle;
    [SerializeField] ParticleSystem shootStartingLight;

    readonly List<ParticleSystem> permanentParticles = new();

    readonly string[] fireButtonConditions = { "Pressed", "Ñlamped", "Released" };

    IEnumerator startShooting;
    readonly float beamLength = 200f;
    readonly float firePointRotateSpeed = 360f;
    readonly float weaponMaxAngle = 20f;
    readonly float damage = 100f;
    Vector3 mousePosition;
    bool onCooldown = false;
    

    private void Start()
    {
        PlayerControl.broadcastMousePosition += TakeMousePosition;
        PlayerControl.broadcastStatusFiringButton += ShootingEvents;
        FillListst();
    }

    private void OnDisable()
    {
        PlayerControl.broadcastMousePosition -= TakeMousePosition;
        PlayerControl.broadcastStatusFiringButton -= ShootingEvents;
    }

    private void FixedUpdate()
    {
        RotateToMouse();
    }

    void ShootingEvents(string condition)
    {
        if (condition == fireButtonConditions[0])
        {
            startShooting = StartShooting();
            StartCoroutine(startShooting);
        }

        if (condition == fireButtonConditions[1]) ContinuedShooting();

        if (condition == fireButtonConditions[2])
        {
            StartCoroutine(StopShooting());
        }
    }

    IEnumerator StartShooting()
    {
        while (onCooldown) { yield return null; }
        onCooldown = true;

        shootStartingParticle.Play();
        shootStartingLight.Play();  
        PlayAudioEffect(false, startShoot);

        yield return new WaitForSeconds(startShoot.length);

        shootStartingParticle.Stop();
        PlayPermamentParticles(true);
        PlayAudioEffect(true, continueShoot);

        lineRenderer.enabled = true;
    }

    void PlayAudioEffect(bool looping, AudioClip audioClipName)
    {
        audioSource.Stop();
        audioSource.loop = looping;
        audioSource.clip = audioClipName;
        audioSource.Play();
    }

    void PlayPermamentParticles(bool enable)
    {
        for (int i = 0; i < permanentParticles.Count; i++)
        {
            if (enable) permanentParticles[i].Play();
            if (!enable) permanentParticles[i].Stop();
        }
    }

    void ContinuedShooting()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * beamLength);

        if (Physics.Raycast(transform.position, firePoint.forward, out RaycastHit raycastHit, float.MaxValue) & lineRenderer.enabled == true)
        {
            lineRenderer.SetPosition(1, raycastHit.point);
            IDadamageable hit = raycastHit.collider.GetComponent<IDadamageable>();
            hit?.Damage(damage * Time.deltaTime);
        }

        permanentEndPointParticlesContainer.transform.position = lineRenderer.GetPosition(1);
    }

    IEnumerator StopShooting()
    {
        lineRenderer.enabled = false;
        StopCoroutine(startShooting);

        shootStartingParticle.Stop();
        shootStartingLight.Stop();
        PlayPermamentParticles(false);
        PlayAudioEffect(false, endShoot);        

        yield return new WaitForSeconds(endShoot.length);
        onCooldown = false;
    }

    void RotateToMouse()
    {
        Vector3 mouseDirection = mousePosition - firePoint.position;
        Vector3 mouseDirection2D = new Vector3(mouseDirection.x, 0, mouseDirection.z).normalized;
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
        for (int i = 0; i < permanentStartPointParticlesContainer.transform.childCount; i++)
        {
            if (permanentStartPointParticlesContainer.transform.GetChild(i).TryGetComponent<ParticleSystem>(out var ps))
            {
                permanentParticles.Add(ps);                
            }
        }

        for (int i = 0; i < permanentEndPointParticlesContainer.transform.childCount; i++)
        {
            if (permanentEndPointParticlesContainer.transform.GetChild(i).TryGetComponent<ParticleSystem>(out var ps))
            {
                permanentParticles.Add(ps);
            }
        }
    }
}



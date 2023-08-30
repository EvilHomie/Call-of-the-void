using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLaser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource hitAudio;
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
    

    void OnEnable()
    {
        PlayerControl.broadcastMousePosition += TakeMousePosition;
        PlayerControl.broadcastStatusFiringButton += ShootingEvents;
        FillListst();
    }

    void OnDisable()
    {
        PlayerControl.broadcastMousePosition -= TakeMousePosition;
        PlayerControl.broadcastStatusFiringButton -= ShootingEvents;
    }

    void FixedUpdate()
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

    void ContinuedShooting()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * beamLength);
        

        if (Physics.Raycast(transform.position, firePoint.forward, out RaycastHit raycastHit, beamLength) & lineRenderer.enabled == true)
        {
            lineRenderer.SetPosition(1, raycastHit.point);
            IDadamageable hit = raycastHit.collider.GetComponent<IDadamageable>();


            hit?.Damage(damage * Time.deltaTime);
            hitAudio.Play();
        }
        else { hitAudio.Stop(); }

        endPoint.transform.position = lineRenderer.GetPosition(1);
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

    void PlayAudioEffect(bool looping, AudioClip audioClipName)
    {
        audioSource.Stop();
        audioSource.loop = looping;
        audioSource.clip = audioClipName;
        audioSource.Play();
        hitAudio.Stop();
    }

    void PlayPermamentParticles(bool enable)
    {
        for (int i = 0; i < permanentParticles.Count; i++)
        {
            if (enable) permanentParticles[i].Play();
            if (!enable) permanentParticles[i].Stop();
        }
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
        for (int i = 0; i < startPoint.transform.childCount; i++)
        {
            if (startPoint.transform.GetChild(i).TryGetComponent<ParticleSystem>(out var ps))
            {
                permanentParticles.Add(ps);                
            }
        }

        for (int i = 0; i < endPoint.transform.childCount; i++)
        {
            if (endPoint.transform.GetChild(i).TryGetComponent<ParticleSystem>(out var ps))
            {
                permanentParticles.Add(ps);
            }
        }
    }
}



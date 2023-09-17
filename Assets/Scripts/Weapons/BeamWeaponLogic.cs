using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeaponLogic : MonoBehaviour, IWeapon
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    [SerializeField] AudioSource weaponAS;
    [SerializeField] AudioSource endPointAS;
    [SerializeField] AudioClip startShoot;
    [SerializeField] AudioClip endShoot;
    [SerializeField] AudioClip continueShoot;
    

    [SerializeField] ParticleSystem chargeParticles;
    [SerializeField] ParticleSystem lightPoint;

    readonly List<ParticleSystem> permanentParticles = new();

    IEnumerator shooting;
    readonly float beamLength = 150f;

    [SerializeField] float energyDamage;
    [SerializeField] float kineticDamage;

    bool isShooting = false;

    void Awake()
    {
        FillPermamentParticlesList();
    }
    public void SetParameters(float energyDmg, float kineticDmg)
    {
        energyDamage = energyDmg;
        kineticDamage = kineticDmg;
    }

    void PlayPermamentParticles(bool enable)
    {
        for (int i = 0; i < permanentParticles.Count; i++)
        {
            if (enable) permanentParticles[i].Play();
            if (!enable) permanentParticles[i].Stop();
        }
    }  

    void FillPermamentParticlesList()
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

    public void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            shooting = Shooting();
            StartCoroutine(shooting);
        }
    }
    public void Stop()
    {
        isShooting = false;
        StartCoroutine(StopShooting());
    }

    IEnumerator Shooting()
    {        
        chargeParticles.Play();
        lightPoint.Play();
        PlayAudioEffect(false, startShoot);
        yield return new WaitForSeconds(startShoot.length);

        chargeParticles.Stop();
        PlayPermamentParticles(true);
        PlayAudioEffect(true, continueShoot);

        lineRenderer.enabled = true;

        while (isShooting & lineRenderer.enabled == true)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * beamLength);  

            if (Physics.Raycast(transform.position, firePoint.forward, out RaycastHit raycastHit, beamLength, 1))
            {
                lineRenderer.SetPosition(1, raycastHit.point);
                IDadamageable hit = raycastHit.collider.GetComponent<IDadamageable>();

                hit?.Damage(energyDamage * Time.deltaTime, kineticDamage * Time.deltaTime);
                if (!endPointAS.isPlaying) endPointAS.Play();
            }
            else { if (endPointAS.isPlaying) endPointAS.Stop(); }

            endPoint.transform.position = lineRenderer.GetPosition(1);
            yield return null;
        }   
    }

    IEnumerator StopShooting()
    {
        lineRenderer.enabled = false;
        StopCoroutine(shooting);

        chargeParticles.Stop();
        lightPoint.Stop();
        PlayPermamentParticles(false);
        PlayAudioEffect(false, endShoot);

        yield return new WaitForSeconds(endShoot.length);
        
    }

    void PlayAudioEffect(bool looping, AudioClip audioClipName)
    {
        weaponAS.Stop();
        weaponAS.loop = looping;
        weaponAS.clip = audioClipName;
        weaponAS.Play();
        endPointAS.Stop();
    }    
}



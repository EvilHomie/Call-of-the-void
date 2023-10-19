using System.Collections;
using UnityEngine;

public class BeamWeaponLogic : MonoBehaviour, IWeapon
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] LayerMask interactLayer;

    [SerializeField] AudioSource weaponAS;
    [SerializeField] AudioSource endPointAS;
    [SerializeField] AudioClip startShoot;
    [SerializeField] AudioClip endShoot;
    [SerializeField] AudioClip continueShoot;
    

    [SerializeField] ParticleSystem chargeParticles;
    [SerializeField] ParticleSystem lightPoint;
    [SerializeField] ParticleSystem shootParticles;

    IEnumerator shooting;

    [SerializeField] float beamLength;
    [SerializeField] float energyDamage;
    [SerializeField] float kineticDamage;

    bool isShooting = false;

    public void SetWeaponParameters(float fireRate, float projectileSpeed, float projectileLifeTime, float energyDMG, float kineticDMG, float beamLength)
    {
        energyDamage = energyDMG;
        kineticDamage = kineticDMG;
        this.beamLength = beamLength;
    }

    private void OnDisable()
    {
        if (isShooting)
        {
            isShooting = false;
            lineRenderer.enabled = false;
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
        shootParticles.Play();
        PlayAudioEffect(true, continueShoot);

        lineRenderer.enabled = true;

        while (isShooting)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * beamLength);  

            if (Physics.Raycast(transform.position, firePoint.forward, out RaycastHit raycastHit, beamLength, interactLayer))
            {
                lineRenderer.SetPosition(1, raycastHit.point);
                IDadamageable hit = raycastHit.collider.transform.root.gameObject.GetComponent<IDadamageable>();

                hit?.Damage(energyDamage * Time.deltaTime, kineticDamage * Time.deltaTime);
                endPoint.transform.position = lineRenderer.GetPosition(1);
                if (!endPoint.activeSelf) endPoint.SetActive(true);
            }
            else { if (endPoint.activeSelf) endPoint.SetActive(false); }
            
            yield return null;
        }   
    }

    IEnumerator StopShooting()
    {
        lineRenderer.enabled = false;
        if (endPoint.activeSelf) endPoint.SetActive(false);
        StopCoroutine(shooting);

        chargeParticles.Stop();
        lightPoint.Stop();
        shootParticles.Stop();
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



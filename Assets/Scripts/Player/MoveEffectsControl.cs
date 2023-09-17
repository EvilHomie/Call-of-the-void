using System.Collections.Generic;
using UnityEngine;

public class MoveEffectsControl : MonoBehaviour
{
    [SerializeField] ParticleSystem[] leftRCS;
    [SerializeField] ParticleSystem[] rightRCS;
    [SerializeField] ParticleSystem[] forwardRCS;
    [SerializeField] ParticleSystem[] backRCS;
    [SerializeField] ParticleSystem mainEngine;
    [SerializeField] AudioSource engineAudioSource;
    [SerializeField] AudioSource rCSAudioSource;
    [SerializeField] PlayerControl playerControl;

    [SerializeField] List<ParticleSystem> allParticles = new List<ParticleSystem>();


    bool controlOnline = true;
    

    private void Start()
    {
        FillList();
    }

    private void Update()
    {
        if (playerControl.enabled)
        {
            controlOnline = true;
            EngineParticlesControll();
            RCSParticlesControll();
        }
        else if (!playerControl.enabled)
        {
            DisableAll();
            controlOnline = false;
        }

        SoundEnabler();
        SoundDisabler();
    }

    void FillList()
    {
        foreach (var pS in leftRCS) allParticles.Add(pS);
        foreach (var pS in rightRCS) allParticles.Add(pS);
        foreach (var pS in forwardRCS) allParticles.Add(pS);
        foreach (var pS in backRCS) allParticles.Add(pS);
    }

    void EngineParticlesControll()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            mainEngine.Play();
            engineAudioSource.Play(); 
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            mainEngine.Stop();
            engineAudioSource.Stop();
        }
    }

    void RCSParticlesControll()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RCSSwitcher(rightRCS, true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            RCSSwitcher(rightRCS, false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RCSSwitcher(leftRCS, true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            RCSSwitcher(leftRCS, false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RCSSwitcher(backRCS, true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            RCSSwitcher(backRCS, false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RCSSwitcher(forwardRCS, true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            RCSSwitcher(forwardRCS, false);
        }
    }

    void RCSSwitcher(ParticleSystem[] rCSArray, bool mustPlay)
    {
        if (mustPlay)
        {
            foreach (var pS in rCSArray) pS.Play();
        }
        else
        {
            foreach (var pS in rCSArray) pS.Stop();
        }
    }

    void SoundDisabler()
    {
        if (rCSAudioSource.isPlaying && allParticles.TrueForAll(element => element.isEmitting == false))
        {
            rCSAudioSource.Stop();
        }
    }

    void DisableAll()
    {        
        if (controlOnline)
        {
            List<ParticleSystem> result = allParticles.FindAll(element => element.isEmitting == true);
            foreach (var pS in result) pS.Stop();

            mainEngine.Stop();
            engineAudioSource.Stop();
        }        
    }

    void SoundEnabler()        
    {
        if (!rCSAudioSource.isPlaying && allParticles.Exists(element => element.isEmitting == true))
        {
            rCSAudioSource.Play();
        }
    }
}

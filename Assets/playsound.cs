using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsound : MonoBehaviour
{
    AudioSource m_AudioSource;
    [SerializeField] AudioClip m_Clip;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_AudioSource.PlayOneShot(m_Clip);
        }
    }
}

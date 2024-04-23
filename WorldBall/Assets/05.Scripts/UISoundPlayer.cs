using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        GetComponent<CustomButton>().EventButtonDown += SoundPlay;
    }

    public void SoundPlay()
    {
        m_audioSource.Play();
    }
}

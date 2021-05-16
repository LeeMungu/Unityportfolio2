using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoundSet : MonoBehaviour
{
    [SerializeField] AudioClip[] m_startClips;
    [SerializeField] AudioClip[] m_scoreClips;
    [SerializeField] AudioClip[] m_endClips;
    [SerializeField] AudioClip[] m_idleClips;
    AudioSource m_audioSource;
    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    public void StartSoundPlay()
    {
        m_audioSource.clip = m_startClips[Random.Range(0, m_startClips.Length)];
        m_audioSource.Play();
    }
    public void ScoreSoundPlay()
    {

        m_audioSource.clip = m_scoreClips[Random.Range(0, m_scoreClips.Length)];
        m_audioSource.Play();
    }
    public void EndSoundPlay()
    {
        m_audioSource.clip = m_endClips[Random.Range(0, m_endClips.Length)];
        m_audioSource.Play();
    }
    public void IdleSoundPlay()
    {
        m_audioSource.clip = m_idleClips[Random.Range(0, m_idleClips.Length)];
        m_audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    static SoundManager s_instance = null;
    public static SoundManager instance { get { return s_instance; } }

    [Header("SoundObjects")]
    [SerializeField] GameObject[] m_BgmObjectList;
    [SerializeField] GameObject[] m_VoiceObjectList;
    [SerializeField] GameObject[] m_SeObjectList;
    [Header("Sliders")]
    [SerializeField] Slider m_BgmSlider;
    [SerializeField] Slider m_VoiceSlider;
    [SerializeField] Slider m_SeSlider;
    float m_BgmVolum;
    float m_VoiceVolum;
    float m_SeVolum;

    private void Awake()
    {
        s_instance = this;
    }

    private void Start()
    {
        SoundSet();
        UIManager.instance.FindObjcet("ConfigPanel").SetActive(false);
    }

    private void Update()
    {
        if(UIManager.instance.FindObjcet("ConfigPanel").activeSelf==true)
        {
            SoundSet();
        }
    }

    private void SoundSet()
    {
        if (m_BgmVolum != m_BgmSlider.value)
        {
            m_BgmVolum = m_BgmSlider.value;
            if (m_BgmObjectList.Length != 0)
            {
                for (int i = 0; i < m_BgmObjectList.Length; ++i)
                {
                    m_BgmObjectList[i].GetComponent<AudioSource>().volume = m_BgmVolum;
                }
            }
        }
        if (m_VoiceVolum != m_VoiceSlider.value)
        {
            m_VoiceVolum = m_VoiceSlider.value;
            if (m_VoiceObjectList.Length != 0)
            {
                for (int i = 0; i < m_VoiceObjectList.Length; ++i)
                {
                    m_VoiceObjectList[i].GetComponent<AudioSource>().volume = m_VoiceVolum;
                }
            }
        }
        if (m_SeVolum != m_SeSlider.value)
        {
            m_SeVolum = m_SeSlider.value;
            if (m_SeObjectList.Length != 0)
            {
                for (int i = 0; i < m_SeObjectList.Length; ++i)
                {
                    m_SeObjectList[i].GetComponent<AudioSource>().volume = m_SeVolum;
                }
            }
        }
    }
}

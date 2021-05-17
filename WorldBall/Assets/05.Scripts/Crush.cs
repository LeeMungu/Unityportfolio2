using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush : MonoBehaviour
{
    //Rigidbody m_rigidbody;
    [Header("Point")]
    [SerializeField] int m_score = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UIManager.instance.ScorePlus(m_score);
            other.GetComponent<PlayerSoundSet>().ScoreSoundPlay();
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowCamera : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    [SerializeField] float m_upPosition;
    private void LateUpdate()
    {
        //transform.position = m_target.transform.position+m_target.transform.up * m_upPosition;
        //transform.up = m_target.transform.forward;
        //transform.LookAt(m_target.transform);
        transform.position = m_target.transform.position;
        transform.rotation = m_target.transform.rotation;
    }
}

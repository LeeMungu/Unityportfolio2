using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowCamera : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    [SerializeField] float m_upPosition;
    [SerializeField] float m_followSpeed = 10;
    [SerializeField] float m_rotateSpeed = 360f;

    private void LateUpdate()
    {
        //transform.position = m_target.transform.position+m_target.transform.up * m_upPosition;
        //transform.up = m_target.transform.forward;
        transform.position = Vector3.Lerp(transform.position,m_target.transform.position + m_target.transform.up * m_upPosition - 
            -m_target.transform.forward * 0.5f,m_followSpeed*Time.deltaTime);
        //transform.LookAt(Vector3.zero);

        //transform.rotation = Quaternion.Euler(transform.eulerAngles);
        Vector3 dir = Vector3.zero - transform.position;
        //transform.rotation = Quaternion.LookRotation(dir.normalized);
        transform.LookAt(m_target.transform.position, m_target.transform.up);


        //float x = Input.GetAxis("Mouse X");
        //float y = Input.GetAxis("Mouse Y");

        //transform.RotateAround(Vector3.zero, Vector3.right, m_rotateSpeed * Time.deltaTime);
    }
}

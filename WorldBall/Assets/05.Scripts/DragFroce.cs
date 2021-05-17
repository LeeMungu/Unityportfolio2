using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFroce : MonoBehaviour
{
    
    [Header("Physics")]
    [SerializeField] float m_Speed = 10f;
    [SerializeField] float m_gravity = 9.8f;
    [SerializeField] float m_groundRidus = 25.5f;
    [SerializeField] float m_rotationSpeed = 150f;
    float m_rotation=0;
    bool m_isGround = false;
    
    private void Start()
    {
        m_isGround = false;
    }

    private void Update()
    {
        if(gameObject.activeSelf==false)
        {
            m_isGround = false;
        }
        float distance = Vector3.Distance(Vector3.zero, transform.position);
        if (distance < m_groundRidus)
        {
            m_isGround = true;
        }

        if (!m_isGround)
        {
            transform.position -= transform.position.normalized * m_Speed;
            m_Speed += 0.01f;
        }
        //중력에 따른 회전
        RotateBody();
        if (m_isGround)
        {
            //땅위에 있을때 고도 유지
            transform.position =
                (transform.position).normalized * m_groundRidus;

            m_rotation += m_rotationSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0f, m_rotation, 0f), Space.Self);
        }
    }
    void RotateBody()
    {
        transform.up = (transform.position.normalized);
    }

    
}

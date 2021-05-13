using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFroce : MonoBehaviour
{
    //Rigidbody m_rigidbody;
    [SerializeField] float m_Speed = 10f;
    [SerializeField] int m_score = 1;
    [SerializeField] float m_gravity = 9.8f;
    [SerializeField] float m_groundRidus = 25.5f;
    bool m_isGround = false;
    private void Awake()
    {
        //m_rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(Vector3.zero, transform.position);
        if (distance < m_groundRidus)//상수나 변수로 제한줄것
        {
            m_isGround = true;
            //m_rigidbody.velocity = Vector3.zero;
        }

        if (!m_isGround)
        {
            //추락
            //m_rigidbody.AddForce(-transform.position.normalized * m_Speed);
            transform.position -= transform.position.normalized * m_Speed;
        }
        //중력에 따른 회전
        RotateBody();
        if (m_isGround)
        {
            //땅위에 있을때 고도 유지
            transform.position =
                (transform.position).normalized * m_groundRidus;
        }
    }
    void RotateBody()
    {
        transform.up = (transform.position.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            UIManager.instance.ScorePlus(m_score);
            gameObject.SetActive(false);
        }
    }
}

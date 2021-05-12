using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody m_rigidbody;
    [SerializeField] float m_Speed = 10f;
    [SerializeField] float m_rotationSpeed = 5f;
    bool m_isGround = false;
    Transform m_cameraPosition;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_cameraPosition = transform.Find("CameraPosition");
    }
    private void Update()
    {
        float distance = Vector3.Distance(Vector3.zero, transform.position);
        if (distance < 11f)
        {
            m_isGround = true;
            m_rigidbody.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_isGround = false;
            m_rigidbody.AddForce(transform.up*500f);
        }
        float ahorizontal = Input.GetAxis("Horizontal");
        float bvertical = Input.GetAxis("Vertical");
        
        if (!m_isGround)
        {
            //추락
            m_rigidbody.AddForce(-transform.position.normalized * m_Speed );
        }
        //중력에 따른 캐릭터 회전
        RotateBody(ahorizontal);

        transform.Rotate(0,m_rotationSpeed * ahorizontal, 0,Space.Self);
        Debug.Log(m_rotationSpeed * ahorizontal);
        //m_cameraPosition.Rotate(0, -m_rotationSpeed * ahorizontal, 0);
        //m_cameraPosition.rotation = Quaternion.Euler(new Vector3(0,transform.rotation.y,0));
        if(m_isGround)
        {
            //땅위에 있을때 자동 앞으로 가기
            transform.position = 
                (transform.position + transform.forward.normalized * m_Speed * Time.deltaTime).normalized * 11f;
        }
    }
    void RotateBody(float ah)
    {
        float rateY = transform.rotation.y+ m_rotationSpeed * ah;
        transform.up = (transform.position.normalized);
    }
}

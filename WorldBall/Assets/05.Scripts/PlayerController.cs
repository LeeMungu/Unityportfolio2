using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rigidbody;

    //[SerializeField] float m_distanceForCenter = 11f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_rotateSpeed = 1f;
    [SerializeField] float m_radiuse = 0.03f;
    float m_yawValue = 0f;
    float m_pitchValue = 0f;
    float horizontal = 0f;
    bool m_isButtonUp = false;
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonStay += HorizontalMinus;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonStay += HorizontalPlus;
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonUp += ButtonUp;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonUp += ButtonUp;
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonDown += ButtonDown;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonDown += ButtonDown;
    }

    private void Update()
    {
        if (GameManager.instance.isGamePlaying)
        {
            //float horizontal = Input.GetAxis("Horizontal");//좌우
            //float vertical = Input.GetAxis("Vertical");//앞뒤

            //업됫을때만
            if (m_isButtonUp)
            {
                if (horizontal > 0)
                    horizontal -= m_radiuse;
                else if (horizontal < 0)
                    horizontal += m_radiuse;
                else
                    m_isButtonUp = false;
            }
            Debug.Log(horizontal);
            transform.Rotate(new Vector3(0f, horizontal * m_rotateSpeed * Time.deltaTime, 0f), Space.Self);
            transform.RotateAround(Vector3.zero, transform.right,
                //vertical 
                1f * m_speed * Time.deltaTime);
        }
    }

    void HorizontalPlus()
    {
        if (horizontal < 1f)
            horizontal += m_radiuse;
    }
    void HorizontalMinus()
    {
        if (horizontal > -1f)
            horizontal -= m_radiuse;
    }
    void ButtonDown()
    {
        m_isButtonUp = false;
    }
    void ButtonUp()
    {
        m_isButtonUp = true;
    }
}

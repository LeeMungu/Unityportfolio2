using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    readonly int m_animeHashKeyState = Animator.StringToHash("State");
    readonly int m_animeHashKeyRandomIdle = Animator.StringToHash("RandomIdle");

    Animator m_animator;
    enum State
    {
        Idle=0,
        Playing=1,
        Config=2
    }
    Rigidbody m_rigidbody;

    //[SerializeField] float m_distanceForCenter = 11f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_rotateSpeed = 1f;
    [SerializeField] float m_radiuse = 0.03f;
    float m_yawValue = 0f;
    float m_pitchValue = 0f;
    float horizontal = 0f;
    bool m_isButtonUp = false;

    State m_state = State.Idle;
    int m_randomIdle = 0;
    Coroutine m_randomIdleCoroutine = null;
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonStay += HorizontalMinus;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonStay += HorizontalPlus;
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonUp += ButtonUp;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonUp += ButtonUp;
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonDown += ButtonDown;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonDown += ButtonDown;
        UIManager.instance.FindObjcet("PlayerTerchPanel").GetComponent<CustomButton>().EventButtonDown += RandomIdleSet;
    }

    private void Update()
    {

        if (GameManager.instance.gameMode == GameManager.GameMode.Playing)
        {
            ChangeState(State.Playing);
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
        else if(GameManager.instance.gameMode == GameManager.GameMode.Start)
        {
            ChangeState(State.Idle);
        }
        else if (GameManager.instance.gameMode == GameManager.GameMode.Config)
        {
            ChangeState(State.Idle);
        }
    }
    void ChangeState(State state)
    {
        if (m_state == state)
            return;

        m_state = state;
        m_animator.SetInteger(m_animeHashKeyState, (int)m_state);
        switch(m_state)
        {
            case State.Idle:
                if (m_randomIdleCoroutine == null)
                    m_randomIdleCoroutine = StartCoroutine(IrandomIdleEter());
                break;
            case State.Playing:
                StopCoroutine(m_randomIdleCoroutine);
                m_randomIdleCoroutine = null;
                break;
            case State.Config:
                StopCoroutine(m_randomIdleCoroutine);
                m_randomIdleCoroutine = null;
                break;
        }
    }
    IEnumerator IrandomIdleEter()
    {
        while (true)
        {
            yield return m_randomIdle == 0;
            yield return new WaitForSeconds(10f);
            RandomIdleSet();
        }
    }
    void RandomIdleSet()
    {
        m_randomIdle = Random.Range(1, 4);
        m_animator.SetInteger(m_animeHashKeyRandomIdle, m_randomIdle);
        GetComponent<PlayerSoundSet>().IdleSoundPlay();
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
    void OnRandomIdleAnimationEnd()
    {
        ChangeState(State.Idle);
        m_randomIdle = 0;
        m_animator.SetInteger(m_animeHashKeyRandomIdle,m_randomIdle);
    }
}
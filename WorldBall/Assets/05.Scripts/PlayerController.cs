using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 컨트롤 클레스
/// </summary>
public class PlayerController : MonoBehaviour
{
    readonly int m_animeHashKeyState = Animator.StringToHash("State");
    readonly int m_animeHashKeyRandomIdle = Animator.StringToHash("RandomIdle");
    readonly int m_animeHashKeyDie = Animator.StringToHash("Die");

    Animator m_animator;
    
    /// <summary>
    /// 플레이어의 상태
    /// </summary>
    enum State
    {
        Idle=0,
        Playing=1,
        Config=2
    }

    /// <summary>
    /// 터치 조작의 상태
    /// </summary>
    enum TerchMode
    {
        Left,
        Right,
        Noon
    }

    Rigidbody m_rigidbody;

    //[SerializeField] float m_distanceForCenter = 11f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_rotateSpeed = 1f;
    [SerializeField] float m_radiuse = 0.03f;
    [SerializeField] float m_plusSpeed = 10f;
    float horizontal = 0f;
    //bool m_isButtonUp = false;

    TerchMode m_terchMode = TerchMode.Noon;
    State m_state = State.Idle;
    int m_randomIdle = 0;
    Coroutine m_randomIdleCoroutine = null;

    /// <summary>
    /// TODO : 특정 개체에서 사용 버튼 콜백 넣어주는건 확장하려면 구조상 문제가 있어서 개선할 필요가 있다.
    /// </summary>
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonStay += HorizontalMinus;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonStay += HorizontalPlus;
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonUp += ButtonUpLeft;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonUp += ButtonUpRight;
        UIManager.instance.FindObjcet("LeftTepPanel").GetComponent<CustomButton>().EventButtonDown += ButtonDownLeft;
        UIManager.instance.FindObjcet("RightTepPanel").GetComponent<CustomButton>().EventButtonDown += ButtonDownRight;
        UIManager.instance.FindObjcet("PlayerTerchPanel").GetComponent<CustomButton>().EventButtonDown += RandomIdleSet;
    }

    private void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Playing)
        {
            ChangeState(State.Playing);
            //float horizontal = Input.GetAxis("Horizontal");//좌우
            //float vertical = Input.GetAxis("Vertical");//앞뒤

            //업되었을 때만
            if (m_terchMode == TerchMode.Noon)
            {
                if (horizontal > 0)
                    horizontal -= m_radiuse;
                else if (horizontal < 0)
                    horizontal += m_radiuse;
                else
                    m_terchMode = TerchMode.Noon;
            }
            else if(m_terchMode == TerchMode.Left)
            {
                if (horizontal > -1f)
                    horizontal -= m_radiuse;
            }
            else if(m_terchMode == TerchMode.Right)
            {
                if (horizontal < 1f)
                    horizontal += m_radiuse;
            }

            //Debug.Log(horizontal);
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

    void ButtonDownLeft()
    {
        if (m_terchMode == TerchMode.Right)
            horizontal = 0;
        m_terchMode = TerchMode.Left;
    }

    void ButtonDownRight()
    {
        if (m_terchMode == TerchMode.Left)
            horizontal = 0;
        m_terchMode = TerchMode.Right;
    }

    void ButtonUpLeft()
    {
        if (m_terchMode == TerchMode.Left)
            m_terchMode = TerchMode.Noon;
    }

    void ButtonUpRight()
    {
        if (m_terchMode == TerchMode.Right)
            m_terchMode = TerchMode.Noon;
    }

    public void SetDie()
    {
        m_animator.SetTrigger(m_animeHashKeyDie);
    }

    void OnRandomIdleAnimationEnd()
    {
        ChangeState(State.Idle);
        m_randomIdle = 0;
        m_animator.SetInteger(m_animeHashKeyRandomIdle,m_randomIdle);
    }

    /// <summary>
    /// 시간에 따른 이속증가
    /// </summary>
    public void PlusSpeed()
    {
        m_speed += m_plusSpeed;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Unity서 제공하는 Button 컴포넌트 만으로는 기능이 제한적이므로 별도로 구현
/// </summary>
public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// 버튼 눌렸을 때 실행할 이벤트
    /// </summary>
    public event System.Action EventButtonDown;

    /// <summary>
    /// 버튼 누르고 있을 때 실행할 이벤트
    /// </summary>
    public event System.Action EventButtonStay;

    /// <summary>
    /// 버튼 땠을 때 실행할 이벤트
    /// </summary>
    public event System.Action EventButtonUp;

    /// <summary>
    /// 버튼이 눌렸는지
    /// </summary>
    bool m_isDown = false;

    /// <summary>
    /// 후에 싱글톤 등에서 읽어오는 식으로 교체 예정
    /// </summary>
    Camera m_uiCamera;

    void Start()
    {
        m_uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (m_isDown)
        {
            EventButtonStay?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //RectTransformUtility!! 이 안에 CameraCanvas관련 함수들이 제공 
        Vector2 result;
        //1.변환할 좌표 결과를 사용할 오브젝트의 부모 Transform
        //2.스크린 좌표
        //3. UICamera 

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            m_uiCamera,
            out result
            );

        EventButtonDown?.Invoke();
        m_isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventButtonUp?.Invoke();
        m_isDown = false;
    }
}
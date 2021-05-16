using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FallowCamera : MonoBehaviour
{
    public enum CameraMode
    {
        Fallow,
        ChangeConfig,
        Config,
        Start
    }
    [SerializeField] GameObject m_target;
    [SerializeField] GameObject m_configTarget;
    [SerializeField] float m_upPosition;
    [SerializeField] float m_followSpeed = 10;
    [SerializeField] float m_rotateSpeed = 0.3f;
    [SerializeField] float m_configLimitDistance = 3.5f;
    CameraMode m_cameraMode = CameraMode.Fallow;
    private void LateUpdate()
    {
        if (m_cameraMode == CameraMode.Fallow)
        {
            //transform.position = m_target.transform.position+m_target.transform.up * m_upPosition;
            //transform.up = m_target.transform.forward;
            transform.position = Vector3.Lerp(transform.position, m_target.transform.position + m_target.transform.up * m_upPosition -
                -m_target.transform.forward * 0.5f, m_followSpeed * Time.deltaTime);
            //transform.LookAt(Vector3.zero);

            //transform.rotation = Quaternion.Euler(transform.eulerAngles);
            Vector3 dir = Vector3.zero - transform.position;
            //transform.rotation = Quaternion.LookRotation(dir.normalized);
            transform.LookAt(m_target.transform.position, m_target.transform.up);

            //float x = Input.GetAxis("Mouse X");
            //float y = Input.GetAxis("Mouse Y");

            //transform.RotateAround(Vector3.zero, Vector3.right, m_rotateSpeed * Time.deltaTime);
        }
        else if (m_cameraMode == CameraMode.ChangeConfig)
        {
            Debug.Log(Vector3.Distance(transform.position, m_target.transform.position));

            Vector3 temp = m_target.transform.up * 3f + m_target.transform.right ;
            transform.position = Vector3.Lerp(transform.position, m_target.transform.position + temp + m_target.transform.forward * 2.5f,
                m_followSpeed * Time.deltaTime);
            //transform.position -= transform.right * m_rotateSpeed;// * m_followSpeed;
            transform.LookAt(m_target.transform.position + temp, m_target.transform.up);
            if (Mathf.Abs(Vector3.Distance(transform.position, m_target.transform.position + temp + m_target.transform.forward * 2.5f))
                <= float.Epsilon+0.1f)//부동소수점 비교 앱실론
            {
                ChangeCameraMode(CameraMode.Config);
            }
        }
        else if (m_cameraMode == CameraMode.Config)
        {
            
        }
    }
    public void ChangeCameraMode(CameraMode cameraMode)
    {
        if (m_cameraMode == cameraMode)
            return;
        m_cameraMode = cameraMode;
        
        //바뀌는 순간 한번만 실행할 것 들
        switch(m_cameraMode)
        {
            case CameraMode.Fallow:

                break;
            case CameraMode.ChangeConfig:

                break;
            case CameraMode.Config:
                UIManager.instance.FindObjcet("ConfigPanel").SetActive(true);
                break;
        }


    }
}

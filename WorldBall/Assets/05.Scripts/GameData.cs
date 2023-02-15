using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System.Text.RegularExpressions;

public class GameData : MonoBehaviourSingleton<GameData>
{
    [Header("Login&Register")]
    public InputField ID;
    public InputField PW;

    bool InputFieldEmptyCheck(InputField inputField)
    {
        return inputField != null && !string.IsNullOrEmpty(inputField.text);
    }

    /// <summary>
    /// 커스텀 가입
    /// </summary>
    public void CustomSignUp()
    {
        Debug.Log("-------------CustomSignUp-------------");
        if (InputFieldEmptyCheck(ID))
        {
            Debug.Log(Backend.BMember.CustomSignUp(ID.text, "1111", "Temp").ToString());
        }
        else
        {
            Debug.Log("check IDInput or PWInput");
        }
    }

    /// <summary>
    /// 닉네임 체크
    /// </summary>
    private bool CheckNickName()
    {
        return Regex.IsMatch(ID.text, "^[0-9a-zA-Z가-힣]*$");
    }

    /// <summary>
    /// 닉네임 생성
    /// </summary>
    public void OnClickCreatName()
    {
        if(CheckNickName()==false)
        {
            //안됩니다.
            return;
        }

        BackendReturnObject BRO = Backend.BMember.CreateNickname(ID.text);
        if (BRO.IsSuccess())//생성성공
        {
            GameManager.instance.ChangePlayerID(ID.text);
            Debug.Log("생성성공");
        }
        else//생성실패
        {
            switch(BRO.GetStatusCode())
            {
                case "409":
                    Debug.Log("이미 중복된 닉네임이 있는경우");

                    break;
                case "400":
                    if (BRO.GetMessage().Contains("too long")) Debug.Log("20자 이상의 닉네임인 경우");
                    else if (BRO.GetMessage().Contains("blank")) Debug.Log("닉네임에 앞/뒤 공백이 있는경우");
                    break;
                default:
                    Debug.Log("서버 공통 에러 발생: " + BRO.GetErrorCode());
                    break;
            }
        }
    }

    /// <summary>
    /// 닉네임 변경
    /// </summary>
    public void OnClickUpdateName()
    {
        if(CheckNickName()==false)
        {
            Debug.Log("닉네임 한글,영어, 숫자로만 가능");
            return;
        }
        BackendReturnObject BRO = Backend.BMember.UpdateNickname(ID.text);

        if (BRO.IsSuccess())
        {
            Debug.Log("닉네임 변경 완료");
            GameManager.instance.ChangePlayerID(ID.text);
            UIManager.instance.OnIDTextButton();
        }

        else
        {
            switch (BRO.GetStatusCode())
            {
                case "409":
                    Debug.Log("이미 중복된 닉네임이 있는 경우");
                    break;

                case "400":
                    if (BRO.GetMessage().Contains("too long")) Debug.Log("20자 이상의 닉네임인 경우");
                    else if (BRO.GetMessage().Contains("blank")) Debug.Log("닉네임에 앞/뒤 공백이 있는경우");
                    break;

                default:
                    Debug.Log("서버 공통 에러 발생: " + BRO.GetErrorCode());
                    break;
            }
        }
    }

    private void Awake()
    {
        BackendReturnObject bro = Backend.Initialize(true);
        if(bro.IsSuccess())
        {
            Debug.Log("동기 성공");
        }
        else
        {
            Debug.LogError("Failed Backend.Initailize");
        }
    }

    public void InsertData()
    {
        Param param = new Param();
        param.Add("id", GameManager.instance.playerID);
        param.Add("score", UIManager.instance.scoreCount);

        BackendReturnObject BRO = Backend.GameInfo.Insert("data", param);

        if (BRO.IsSuccess()) Debug.Log("데이터 삽입 성공");
        else Debug.Log("데이터 삽입 실패");
    }
    
}

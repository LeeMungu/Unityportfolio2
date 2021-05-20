using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
public class GameData : MonoBehaviourSingleton<GameData>
{
    private void Start()
    {
        BackendReturnObject bro = Backend.Initialize(true);
        if(bro.IsSuccess())
        {

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

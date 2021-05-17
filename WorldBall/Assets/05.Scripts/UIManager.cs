using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] int m_scoreCount = 0;
    public int scoreCount { get { return m_scoreCount; } }
    static UIManager s_instance = null;
    public static UIManager instance { get { return s_instance; } }
    private Dictionary<string, GameObject> m_UIList = new Dictionary<string, GameObject>();
    private void Awake()
    {
        s_instance = this;
        AddList("ScoreText");
        AddList("LeftTepPanel");
        AddList("RightTepPanel");
        AddList("TitlePanel");
        AddList("StartText");
        AddList("StartButton");
        AddList("ConfigPanel");
        AddList("ConfigButton");
        AddList("PlayerTerchPanel");
        AddList("ExitButton");
        AddList("RestartButton");
        AddList("EndButton");
        AddList("GameOverPanel");
        AddList("GameOverRestartButton");
        AddList("RankButton");
        AddList("RankingPanel");
        AddList("RankingText");
        AddList("ExitRankButton");
        //사운드
        AddList("BGM");
        AddList("Vice");
        AddList("SE");
    }
    void Start()
    {
        //버튼에 값넣어주기
        FindObjcet("StartButton").GetComponent<CustomButton>().EventButtonDown += GameManager.instance.SetGameStart;
        FindObjcet("RankButton").GetComponent<CustomButton>().EventButtonDown += GameManager.instance.OnRanking;
        FindObjcet("ExitRankButton").GetComponent<CustomButton>().EventButtonDown += GameManager.instance.ExitRanking;

        FindObjcet("ConfigButton").GetComponent<CustomButton>().EventButtonUp += GameManager.instance.OnConfig;
        FindObjcet("ExitButton").GetComponent<CustomButton>().EventButtonUp += GameManager.instance.ExitConfig;

        FindObjcet("RestartButton").GetComponent<CustomButton>().EventButtonDown += GameManager.instance.OnRestart;
        FindObjcet("GameOverRestartButton").GetComponent<CustomButton>().EventButtonDown += GameManager.instance.OnRestart;
        FindObjcet("EndButton").GetComponent<CustomButton>().EventButtonDown += GameManager.instance.OnEndGame;


        FindObjcet("GameOverPanel").SetActive(false);
        GetComponent<JsonMgr>().Load();
        FindObjcet("RankingText").GetComponent<Text>().text = GetComponent<JsonMgr>().Output();
        //컨피그는 설정때문에 soundManager에서 꺼줌
        FindObjcet("RankingPanel").SetActive(false);
    }


    private void AddList(string temp)
    {
        m_UIList.Add(temp, GameObject.Find(temp));
    }
    public GameObject FindObjcet(string temp)
    {
        if (m_UIList[temp] == null)
            return null;

        return m_UIList[temp];
    }

    public void ScorePlus (int addCount)
    {
        m_scoreCount+=addCount;
        FindObjcet("ScoreText").GetComponent<ScoreScript>().ScoreCount(m_scoreCount);
    }
}

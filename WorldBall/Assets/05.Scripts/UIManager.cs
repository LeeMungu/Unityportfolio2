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
        AddList("TimeText");
        AddList("IDText");
        AddList("IDInput");
        AddList("IDTextButton");
        AddList("IDInputField");
        AddList("IDInputButton");
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

        //NickName부분
        FindObjcet("IDTextButton").GetComponent<CustomButton>().EventButtonUp += OnIDTextButton;

        FindObjcet("GameOverPanel").SetActive(false);
        GetComponent<JsonMgr>().Load();
        FindObjcet("RankingText").GetComponent<Text>().text = GetComponent<JsonMgr>().Output();
        //컨피그는 설정때문에 soundManager에서 꺼줌
        FindObjcet("RankingPanel").SetActive(false);
        FindObjcet("IDInput").SetActive(false);
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
    public void TimeUpdate()
    {
        int m_time = (int)(GameManager.instance.time*60);
        int hr = (m_time / 60 / 60)%60;
        int min = (m_time / 60) % 60;
        int scend = m_time % 60;
        string timeText = null;
        if (hr == 0)
            timeText += "00";
        else if (hr < 10)
            timeText += "0" + hr;
        else
            timeText += hr;
        timeText += ".";
        if (min == 0)
            timeText += "00";
        else if (min < 10)
            timeText += "0" + min;
        else
            timeText += min;
        timeText += ".";
        if (scend== 0)
            timeText += "00";
        else if (scend < 10)
            timeText += "0" + scend;
        else
            timeText += scend;

        FindObjcet("TimeText").GetComponent<Text>().text = timeText;
    }
    public void IDText()
    {
        FindObjcet("IDText").GetComponent<Text>().text = "ID : " + GameManager.instance.playerID.ToString() ;
    }
    public void OnIDTextButton()
    {
        if(FindObjcet("IDInput").activeSelf==false)
        {
            FindObjcet("IDInput").SetActive(true);
        }
        else
        {
            FindObjcet("IDInput").SetActive(false);
        }
    }
}

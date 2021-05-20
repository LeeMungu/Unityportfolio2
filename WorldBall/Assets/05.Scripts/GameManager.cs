using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager s_instance = null;
    public static GameManager instance { get { return s_instance; } }
    public enum GameMode
    {
        Start,
        Playing,
        Config,
        GameOver
    }
    GameMode m_gameMode = GameMode.Start;
    public GameMode gameMode { get { return m_gameMode; } set { m_gameMode = value; } }
    //일시정지용
    bool m_isGamePlaying = false;
    public bool isGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }
    private Dictionary<string, GameObject> m_ObjectList = new Dictionary<string, GameObject>();
    [SerializeField] GameObject[] m_monsterPrepeb;
    [SerializeField] int m_monsterCount = 20;
    private List<GameObject> m_monsterList = new List<GameObject>();

    float m_time = 0f;
    float m_countTime = 0f;
    public float time { get { return m_time; } }
    string m_playerID = "testing";
    public string playerID { get { return m_playerID; } }
    private void Awake()
    {
        s_instance = this;
        AddList("Player1");
        AddList("Ground");
        AddList("Main Camera");
        AddList("Trap");

        if(SystemInfo.deviceUniqueIdentifier!=null)
        m_playerID = SystemInfo.deviceUniqueIdentifier.Substring(0,5);
    }
    private void Start()
    {
        //프레임 제한
        Application.targetFrameRate = 60;
        //에너미 풀생성
        for (int i = 0; i < m_monsterCount*0.7; ++i)
        {
            GameObject temp = Instantiate(m_monsterPrepeb[0],
                new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 70f,
                Quaternion.identity);
            temp.SetActive(false);
            //생성과 동시에 넣어줌
            m_monsterList.Add(temp);
        }
        for (int i = 0; i < m_monsterCount * 0.3; ++i)
        {
            GameObject temp = Instantiate(m_monsterPrepeb[1],
                new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 70f,
                Quaternion.identity);
            temp.SetActive(false);
            //생성과 동시에 넣어줌
            m_monsterList.Add(temp);
        }
    }
    private void Update()
    {
        if(m_gameMode == GameMode.Playing)
        {
            m_time += Time.deltaTime;
            m_countTime += Time.deltaTime;
            UIManager.instance.TimeUpdate();
            if(m_countTime>10)
            {
                m_countTime = 0;
                FindObjcet("Player1").GetComponent<PlayerController>().PlusSpeed();
                FindObjcet("Trap").GetComponent<StromMoveScripts>().SpeedUp();
                Debug.Log("스피드UP");
            }
        }
        
        int countE = 0;
        for (int i = 0; i < m_monsterList.Count; ++i)
        {
            if (m_monsterList[i].activeSelf == true)
            {
                countE++;
                if (countE > m_monsterCount)
                    break;
            }
        }
        if (countE< m_monsterCount)
        {
            for(int i=0; i< m_monsterList.Count; ++i)
            {
                if (m_monsterList[i].activeSelf == false)
                {
                    m_monsterList[i].SetActive(true);
                    m_monsterList[i].transform.position =
                        new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 70f;
                    break;
                }
            }
        }
    }

    private void AddList(string temp)
    {
        m_ObjectList.Add(temp, GameObject.Find(temp));
    }
    public GameObject FindObjcet(string temp)
    {
        if (m_ObjectList[temp] == null)
            return null;

        return m_ObjectList[temp];
    }
    public void SetGameStart()
    {
        m_gameMode = GameMode.Playing;
        m_isGamePlaying = true;
        UIManager.instance.FindObjcet("TitlePanel").SetActive(false);
        FindObjcet("Player1").GetComponent<PlayerSoundSet>().StartSoundPlay();
        FindObjcet("Main Camera").GetComponent<FallowCamera>().ChangeCameraMode(FallowCamera.CameraMode.Fallow);
    }
    public void OnConfig()
    {
        if(m_gameMode == GameMode.Config)
        {
            ExitConfig();
            return;
        }
        m_gameMode = GameMode.Config;
        m_isGamePlaying = false;
        FindObjcet("Main Camera").GetComponent<FallowCamera>().ChangeCameraMode(FallowCamera.CameraMode.ChangeConfig);
    }
    public void ExitConfig()
    {
        m_gameMode = GameMode.Playing;
        m_isGamePlaying = true;
        UIManager.instance.FindObjcet("ConfigPanel").SetActive(false);
        FindObjcet("Main Camera").GetComponent<FallowCamera>().ChangeCameraMode(FallowCamera.CameraMode.Fallow);
    }
    public void OnRanking()
    {
        UIManager.instance.FindObjcet("RankingPanel").SetActive(true);
    }
    public void ExitRanking()
    {
        UIManager.instance.FindObjcet("RankingPanel").SetActive(false);
    }

    public void OnRestart()//씬재시작
    {
        SceneManager.LoadScene(0);
    }
    //어플리케이션 종료
    public void OnEndGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        m_gameMode = GameMode.GameOver;
        //저장
        GetComponent<JsonMgr>().AddRank(m_playerID, UIManager.instance.scoreCount);
        GetComponent<JsonMgr>().Save();// 랭킹 관리를 위해 로드는 UI에
        //캐릭터 모션 추가할 것
        FindObjcet("Player1").GetComponent<PlayerSoundSet>().EndSoundPlay();
        FindObjcet("Player1").GetComponent<PlayerController>().SetDie();
        UIManager.instance.FindObjcet("GameOverPanel").SetActive(true);
        GameData.instance.InsertData();
    }
    //창나갔을때 정지
    public void OnApplicationPause(bool pause)
    {
        m_isGamePlaying = pause;
    }
}

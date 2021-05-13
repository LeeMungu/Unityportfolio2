using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] int m_scoreCount = 0;
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
        AddList("TitleText");
        AddList("TitleButton");
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

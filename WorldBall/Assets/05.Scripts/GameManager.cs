﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance = null;
    public static GameManager instance { get { return s_instance; } }
    bool m_isGamePlaying = false;
    public bool isGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }
    private Dictionary<string, GameObject> m_ObjectList = new Dictionary<string, GameObject>();
    [SerializeField] GameObject m_monsterPrepeb;
    [SerializeField] int m_monsterCount = 20;
    private List<GameObject> m_monsterList = new List<GameObject>();
    private void Awake()
    {
        s_instance = this;
        AddList("Player");
        AddList("Ground");
    }
    private void Start()
    {
        //에너미 풀생성
        for (int i = 0; i < m_monsterCount; ++i)
        {
            GameObject temp = Instantiate(m_monsterPrepeb,
                new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 70f,
                Quaternion.identity);
            temp.SetActive(false);
            //생성과 동시에 넣어줌
            m_monsterList.Add(temp);
        }
        //버튼에 값넣어주기
        UIManager.instance.FindObjcet("TitleButton").GetComponent<CustomButton>().EventButtonUp+=SetGameStart;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) m_isGamePlaying = true;

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
    void SetGameStart()
    {
        m_isGamePlaying = true;
        UIManager.instance.FindObjcet("TitlePanel").SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using BackEnd;

public class JsonMgr : MonoBehaviour
{
    public List<RankData> RankingList = new List<RankData>();
    String m_path = "/RankJson.json";//"/Resources/RankJson.json"
    public void AddRank(string id, int score)
    {
        RankingList.Add(new RankData(id, score));
        //스코어 정렬
        RankingList.Sort((a,b)=>a.Score>b.Score? -1:1);
    }
    public void Save()
    {
        JsonData RankJson = JsonMapper.ToJson(RankingList);
        //Backend.
        //파일에 쓰기
        File.WriteAllText(Application.persistentDataPath + m_path, RankJson.ToString());
    }
    public void Load()
    {
        if (!File.Exists(Application.persistentDataPath + m_path))
        {
            File.Create(Application.persistentDataPath + m_path);
            return;
        }
        if (File.ReadAllText(Application.persistentDataPath + m_path) == "")
        {
            AddRank("Temp", 0);
            JsonData RankJsonx = JsonMapper.ToJson(RankingList);
            File.WriteAllText(Application.persistentDataPath + m_path, RankJsonx.ToString());
            RankingList.Clear();
        }

        string Jsonstring = File.ReadAllText(Application.persistentDataPath + m_path);
        
        JsonData RankJson = JsonMapper.ToObject(Jsonstring);
        if (RankJson != null)
        {
            if (RankJson.Count != 0)
            {
                for (int i = 0; i < RankJson.Count; ++i)
                {
                    AddRank(RankJson[i]["ID"].ToString(), Int32.Parse(RankJson[i]["Score"].ToString()));
                }
            }
        }
    }
    public string Output()
    {
        string temp = null;
        for(int i=0; i<RankingList.Count; ++i)
        {
            int n = i + 1;
            temp +="\t"+ n + "\t\t\t";
            temp += RankingList[i].ID + "\t\t\t";
            temp += RankingList[i].Score + "\n";
        }
        return temp;
    }
}

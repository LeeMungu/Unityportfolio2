using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager s_instance = null;
    public static UIManager instance { get { return s_instance; } }
    private Dictionary<string, GameObject> m_UIList = new Dictionary<string, GameObject>();
    private void Awake()
    {
        s_instance = this;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Text m_text;
    private void Start()
    {
        m_text = GetComponent<Text>();
    }
    private void Update()
    {
        
    }

    public void ScoreCount(int count)
    {
        m_text.text = "Score : " + count;
    }
}

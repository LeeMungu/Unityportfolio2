using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StromMoveScripts : MonoBehaviour
{
    [SerializeField] float m_rotateSpeed =120f;
    [SerializeField] float m_speed=0.2f;
    float horizon = 0f;
    private void Start()
    {
    }
    private void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Playing)
        {
            horizon = Random.Range(-1, 1);
            transform.Rotate(new Vector3(0f, horizon * m_rotateSpeed * Time.deltaTime, 0f), Space.Self);
            transform.RotateAround(Vector3.zero, transform.right, m_speed * Time.deltaTime);
        }
    }
}

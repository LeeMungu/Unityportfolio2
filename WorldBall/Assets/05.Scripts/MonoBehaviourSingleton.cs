using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상속만으로 쉽게 싱글턴 배치하기 위한 클레스
/// NOTE : 타 클래스에서 어디서 쓰이는지 파악하는데 어려움이 있는 문제가 있어서 사용에 고려해봐야 할거 같다.
/// </summary>
public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    GameObject newObject = new GameObject("Manager");
                    _instance = newObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}

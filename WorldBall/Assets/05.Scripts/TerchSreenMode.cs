using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerchSreenMode : MonoBehaviour
{
    private void Start()
    {
        GetComponent<CustomButton>().EventButtonDown += ButtonDown;
        GetComponent<CustomButton>().EventButtonUp += ButtonUp;
    }

    void ButtonDown()
    {
        GetComponent<Image>().color = new Vector4(1f,1f,1f, 0.15f );
        
    }

    void ButtonUp()
    {
        GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 0f);
    }
}

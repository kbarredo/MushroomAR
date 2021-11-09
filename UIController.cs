using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Transform MainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public Popup CreatePopup()
    {
        GameObject popUpGo = Instantiate(Resources.Load("UI/Popup") as GameObject);
        return popUpGo.GetComponent<Popup>();
    }
}

// https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
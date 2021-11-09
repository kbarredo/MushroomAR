using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Popup : MonoBehaviour
{
    public Button _button1;
    public Text _button1Text;
    public Text _popupText;

    public void Init(Transform canvas, string popupMessage, string btn1txt, Action action)
    {
        _popupText.text = popupMessage;
        _button1Text.text = btn1txt;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        // Stretch across screen
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        // Detect click on Exit button
        _button1.onClick.AddListener(() =>
        {
            action(); // Spawn new Mushroom
            GameObject.Destroy(this.gameObject); // Close popup window
        });
    }
}

// https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
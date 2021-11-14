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

    public GameObject[] mushrooms;
    public GameObject currentMushroom;
    public int index;

    public void Init(Transform canvas, string popupMessage, string btn1txt, int isMushroom)
    {
        Action spawn = () =>
        {
            // Close popup window
            GameObject.Destroy(this.gameObject); 

            // Select a random Mushroom to instantiate
            mushrooms = GameObject.FindGameObjectsWithTag("shroom"); // FIXME: only gets mushrooms in scene // mushrooms = new GameObject[] { FlyAgaric, Chanterelle, IndigoMilkCap .. };
            index = UnityEngine.Random.Range(0, mushrooms.Length);
            currentMushroom = mushrooms[index];

            // Instantiate new Mushroom
            index = UnityEngine.Random.Range(0, mushrooms.Length);
            currentMushroom = mushrooms[index];
            Instantiate(currentMushroom, transform.position, transform.rotation); // FIXME: set spawn position
        };

        _popupText.text = popupMessage;
        _button1Text.text = btn1txt;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        // Stretch across screen (disable click outside popup)
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        // Detect click on "X" button
        _button1.onClick.AddListener(() =>
        {
            // Spawn new Mushroom
            if (isMushroom == 1)
            {
                spawn();
            }
            // Close popup window
            GameObject.Destroy(this.gameObject); 
        });
    }
}

// https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
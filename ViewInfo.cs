using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;
using System;

public class ViewInfo : MonoBehaviour
{
    public string text = "";
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>(); // Get Mushroom data from GameManager.cs
    }

    // Detect click on Mushroom collider
    public void OnMouseDown()
    {
        text = gameManager.GetMushroomProps(gameObject.name); // Get Mushroom data from GameManager.cs
        //Debug.Log("Clicked prefab");

        // Initialize popup window
        Popup popup = UIController.Instance.CreatePopup();
        popup.Init(UIController.Instance.MainCanvas,
            text,
            "X", 
            1
            );

        // Remove collected Mushroom from scene
        Destroy(gameObject);
    }

    void Update()
    {
        // Detect tap on Mushroom in AR
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                // Get Mushroom data from GameManager.cs
                text = gameManager.GetMushroomProps(Hit.transform.name);
                // Initialize popup window
                Popup popup = UIController.Instance.CreatePopup();
                popup.Init(UIController.Instance.MainCanvas,
                    text,
                    "X",
                    1
                    );

                // Remove collected Mushroom from scene
                Destroy(gameObject);
            }
        }
    }
}

// Popup - https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
// Destroy(gameObject); - https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html
// Clicking objects in AR (using 3D objects as buttons) - https://www.youtube.com/watch?v=hi_KDpC1nzk

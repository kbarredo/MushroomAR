using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class ViewInfo : MonoBehaviour
{
    public GameObject[] mushrooms;
    public GameObject currentMushroom;
    public int index;
    
    // Start is called before the first frame update
    void Start()
    {
        Action action = () =>
        {
            // Select a random Mushroom to spawn
            mushrooms = GameObject.FindGameObjectsWithTag("shroom"); //mushrooms = new GameObject[] { FlyAgaric, Chanterelle, IndigoMilkCap };
            index = UnityEngine.Random.Range(0, mushrooms.Length);
            currentMushroom = mushrooms[index];
            // Spawn new random mushroom
            index = UnityEngine.Random.Range(0, mushrooms.Length);
            currentMushroom = mushrooms[index];
            Instantiate(currentMushroom, transform.position, transform.rotation); // FIXME: randomize position
        };

        // FIXME Listen for click on Mushroom
        void OnMouseDown()
        {
            Debug.Log("Clicked prefab " + hit.transform.name);
            
            // Create pop up 
            Popup popup = UIController.Instance.CreatePopup();
            // Init pop up with params (canvas, text, text, action)
            popup.Init(UIController.Instance.MainCanvas,
                "[ insert shroom info here ]",
                "X",
                action
                );
        }
    }
}

// https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
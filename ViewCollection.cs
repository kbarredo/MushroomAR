using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewCollection : MonoBehaviour
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

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            // Create pop up with params (canvas, text, text, action)
            Popup popup = UIController.Instance.CreatePopup();
            popup.Init(UIController.Instance.MainCanvas,
                "[ insert collection here ]",
                "X",
                action
                );
        });
    }
}

// https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
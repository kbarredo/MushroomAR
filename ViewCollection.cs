using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO; // File

public class ViewCollection : MonoBehaviour
{
    //private GameManager gameManager;
    public string saveFile = "";
    MushroomList collection = new MushroomList(); // Stores collected Mushroom objects

    void Start()
    {
        // Detect click on "My Collection" button
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            // Deserialize saveFile
            string text = "";
            saveFile = Application.dataPath + "/Scripts/save_data.json";
            if (File.Exists(saveFile))
            {
                // Read entire file and save contents.
                string fileContents = File.ReadAllText(saveFile);
                // Store json data in Mushroom objects.
                collection = JsonUtility.FromJson<MushroomList>(fileContents);

                // Iterate collection to build string
                for (int i = 0; i < collection.data.Count; i++)
                {
                    text += collection.data[i].cname + "\n";
                }
            }

            // Initialize popup window
            Popup popup = UIController.Instance.CreatePopup();
            popup.Init(UIController.Instance.MainCanvas,
                text,
                "X", 
                0
                );
        });
    }
}
// https://www.youtube.com/watch?v=Bm62aXuVX4I&t=0s
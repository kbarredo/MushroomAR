using System.IO; // File
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject infoPrefab; // Mushroom info card

    string dataFile;
    string saveFile;
    MushroomList mushroomJSON = new MushroomList(); // Stores of Mushroom objects
    MushroomList collectedJSON = new MushroomList(); // Stores collected Mushroom objects

    Dictionary<string, Mushroom> allmushrooms = new Dictionary<string, Mushroom>(); // Stores all existing Mushroom names and (deserialized) data 
    Dictionary<string, Mushroom> collected = new Dictionary<string, Mushroom>(); // Stores collected Mushroom names (without duplicates) and (deserialized) data 

    List<string> serialized = new List<string>(); // Stores collected mushrooms as json objects to write to saveFile

    // Declare Mushroom objects.
    Mushroom fly_agaric = new Mushroom();
    Mushroom chanterelle = new Mushroom();
    Mushroom indigo_milk_cap = new Mushroom();
    

    // Start is called before the first frame update
    void Start()
    {
        dataFile = Application.dataPath + "/Scripts/mushroom_data.json";
        saveFile = Application.dataPath + "/Scripts/save_data.json";

        /* FIXME: Populate collection page
        // Check saveFile exists
        if (File.Exists(saveFile))
        {
            // Read entire file and save contents.
            string fileContents = File.ReadAllText(saveFile);
            // Deserialize the json data into a pattern matching the Mushroom class.
            collectedJSON = JsonUtility.FromJson<MushroomList>(fileContents);

            // TODO: for loop populate collection page
            for(int i = 0; i < collectedJSON.length; i++)
            {
                // instantiate collectedJSON.data[i];
            }
        }
        */

        // Check dataFile exists
        if (File.Exists(dataFile))
        {
            // Read entire file and save contents.
            string fileContents = File.ReadAllText(dataFile);

            // Deserialize the json data into a pattern matching the Mushroom class.
            mushroomJSON = JsonUtility.FromJson<MushroomList>(fileContents);

            // Initialize Mushroom objects.
            fly_agaric = mushroomJSON.data[0];
            chanterelle = mushroomJSON.data[1];
            indigo_milk_cap = mushroomJSON.data[2];

            // Store Mushroom objects in a Dictionary (with corresponding prefab name as key)
            allmushrooms.Add("FlyAgaric", fly_agaric);
            allmushrooms.Add("Chanterelle", chanterelle);
            allmushrooms.Add("IndigoMilkCap", indigo_milk_cap);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Listen for click event.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Detect click on Mushroom.
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked " + hit.transform.name);

                // Check if Mushroom already exists in Collection.
                if (!collected.ContainsKey(hit.transform.name))  // FIXME : catch View Collection button
                {
                    if(hit.transform.name == "FlyAgaric" || hit.transform.name == "Chanterelle" || hit.transform.name == "IndigoMilkCap")
                    {
                        // Add Mushroom to Dictionary.
                        collected.Add(hit.transform.name, allmushrooms[hit.transform.name]);

                        // Serialize Mushroom data for saveFile.
                        string json = JsonUtility.ToJson(allmushrooms[hit.transform.name]);
                        serialized.Add(json);

                        // Update saveFile.
                        File.WriteAllText(saveFile, "{\n\t\"data\": [\n\t");
                        for (int i = 0; i < serialized.Count - 2; i++)
                        {
                            File.AppendAllText(saveFile, serialized[i] + ",");
                        }
                        File.AppendAllText(saveFile, serialized[serialized.Count - 1]);
                        //Debug.Log(collected[hit.transform.name].sname + " added to collection.");
                    }
                }

                
            }
        }
    }
}

[System.Serializable]
public class Mushroom
{
    public string cname;
    public string sname;
    public string use;
    public int diameter;
    public string habitat;
    public string model;
    public string imageFilePath;
    public string about;
}

[System.Serializable]
public class MushroomList
{
    public List<Mushroom> data;
}


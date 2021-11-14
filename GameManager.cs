using System.IO; // File
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string dataFile; // Contains all existing Mushroom data
    string saveFile; // Contains all collected Mushroom data

    MushroomList mushroomJSON = new MushroomList(); // Stores all existing Mushroom objects
    MushroomList collectedJSON = new MushroomList(); // Stores collected Mushroom objects

    public Dictionary<string, Mushroom> allmushrooms = new Dictionary<string, Mushroom>(); // Stores all existing Mushroom names and (deserialized) data 
    Dictionary<string, Mushroom> collected = new Dictionary<string, Mushroom>(); // Stores collected Mushroom names (without duplicates) and (deserialized) data 

    List<string> serialized = new List<string>(); // Stores collected Mushroom data as json strings to write to saveFile

    // Declare Mushroom objects.
    Mushroom fly_agaric = new Mushroom();
    Mushroom chanterelle = new Mushroom();
    Mushroom indigo_milk_cap = new Mushroom();
    Mushroom lions_mane = new Mushroom();
    Mushroom shiitake = new Mushroom();
    Mushroom honey_fungus = new Mushroom();
    Mushroom veiled_lady = new Mushroom();
    Mushroom turkey_tail = new Mushroom();

    // Look up Mushroom by name and return its attributes.
    public string GetMushroomProps(string go)
    {
        return "Common name: " + allmushrooms[go].cname + "\n"
            + "Scientific name: " + allmushrooms[go].sname + "\n"
            + "Uses: " + allmushrooms[go].use + "\n"
            + "Habitat: " + allmushrooms[go].habitat + "\n"
            + "About: " + allmushrooms[go].about;
    }

    /************************** 
     * FIXME - only gets mushrooms in scene
     
    // Get list of existing Mushroom prefabs to select one to spawn in popup.cs.
    public GameObject[] GetMushroomList(GameObject[] shrooms)
    {
        return {fly_agaric, chanterelle, indigo_milk_cap, lions_mane, shiitake, honey_fungus, veiled_lady, turkey_tail};
    }
    
    *****************/

    // Start is called before the first frame update
    void Start()
    {
        dataFile = Application.dataPath + "/Scripts/mushroom_data.json";
        saveFile = Application.dataPath + "/Scripts/save_data.json";

        // Check dataFile exists
        if (File.Exists(dataFile))
        {
            // Read entire file and save contents.
            string fileContents = File.ReadAllText(dataFile);

            // Deserialize the json data into a pattern matching the Mushroom class.
            mushroomJSON = JsonUtility.FromJson<MushroomList>(fileContents);

            // Initialize Mushrooms.
            fly_agaric = mushroomJSON.data[0];
            chanterelle = mushroomJSON.data[1];
            indigo_milk_cap = mushroomJSON.data[2];
            turkey_tail = mushroomJSON.data[3];
            lions_mane = mushroomJSON.data[4];
            shiitake = mushroomJSON.data[5];
            honey_fungus = mushroomJSON.data[6];
            veiled_lady = mushroomJSON.data[7];

            // Store all existing Mushrooms in a Dictionary (with corresponding prefab name as key)
            allmushrooms.Add("FlyAgaric", fly_agaric);
            allmushrooms.Add("Chanterelle", chanterelle);
            allmushrooms.Add("IndigoMilkCap", indigo_milk_cap);
            allmushrooms.Add("TurkeyTail", turkey_tail);
            allmushrooms.Add("LionsMane", lions_mane);
            allmushrooms.Add("Shiitake", shiitake);
            allmushrooms.Add("HoneyFungus", honey_fungus);
            allmushrooms.Add("VeiledLady", veiled_lady);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //**********************************************
         
        // FIXME: Detect touch input in AR
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // Check if Mushroom already exists in Collection.
                if (!collected.ContainsKey(hit.transform.name))
                {
                    //GUI.Label(hit.transform.name);
                    if (hit.transform.name == "FlyAgaric" || hit.transform.name == "Chanterelle" || hit.transform.name == "IndigoMilkCap" || hit.transform.name == "TurkeyTail" || hit.transform.name == "LionsMane" || hit.transform.name == "Shiitake" || hit.transform.name == "HoneyFungus" || hit.transform.name == "VeiledLady") // Catches error on click View Collection button
                    {
                        // Add Mushroom to Dictionary.
                        collected.Add(hit.transform.name, allmushrooms[hit.transform.name]);

                        // Serialize Mushroom data for saveFile.
                        string json = JsonUtility.ToJson(allmushrooms[hit.transform.name]);
                        serialized.Add(json);

                        // Update saveFile.
                        File.WriteAllText(saveFile, "{\n\t\"data\": [\n\t"); // FIXME: 
                        for (int i = 0; i < serialized.Count - 1; i++)
                        {
                            File.AppendAllText(saveFile, serialized[i] + ",");
                        }
                        File.AppendAllText(saveFile, serialized[serialized.Count - 1] + "]}");
                    }
                }
            }
        }

        //**********************************************/

        // Listen for click event
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Detect click on Mushroom
            if (Physics.Raycast(ray, out hit))
            {
                // Check if Mushroom already exists in Collection.
                if (!collected.ContainsKey(hit.transform.name))
                {
                    //Debug.Log("Clicked " + hit.transform.name);
                    if (hit.transform.name == "FlyAgaric" || hit.transform.name == "Chanterelle" || hit.transform.name == "IndigoMilkCap" || hit.transform.name == "TurkeyTail" || hit.transform.name == "LionsMane" || hit.transform.name == "Shiitake" || hit.transform.name == "HoneyFungus" || hit.transform.name == "VeiledLady") // Catches error on click View Collection button
                    {
                        // Add Mushroom to Dictionary.
                        collected.Add(hit.transform.name, allmushrooms[hit.transform.name]);

                        // Serialize Mushroom data for saveFile.
                        string json = JsonUtility.ToJson(allmushrooms[hit.transform.name]);
                        serialized.Add(json);

                        // Update saveFile.
                        File.WriteAllText(saveFile, "{\n\t\"data\": [\n\t"); // FIXME: 
                        for (int i = 0; i < serialized.Count - 1; i++)
                        {
                            File.AppendAllText(saveFile, serialized[i] + ",");
                        }
                        File.AppendAllText(saveFile, serialized[serialized.Count - 1] + "]}");
                        //Debug.Log(collected[hit.transform.name].sname + " added to collection.");
                    }
                }
            }
        }
    }
}

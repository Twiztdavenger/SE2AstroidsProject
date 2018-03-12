using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;


public class TrialController : MonoBehaviour {
    bool isPaused = false;

    public GameObject trial;

    public string stringToEdit = "Hello World";

    public bool trialStart = false;

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    private Queue<GameObject> trials;

    // Use this for initialization
    void Start () {
         
        try{
            var doc = new XmlDocument();
            doc.Load(XML_FILE_PATH);
            XmlNodeList xmlTrials = doc.SelectNodes("experiment/trial");
            foreach(XmlNode xmlTrial in xmlTrials)
            {
                XmlNode ship = xmlTrial.SelectSingleNode("ship");
                XmlNode asteroid = xmlTrial.SelectSingleNode("asteroid");

                float shipSpawnX = float.Parse(ship.SelectSingleNode("spawn").Attributes["x"]?.InnerText);
                float shipSpawnY = float.Parse(ship.SelectSingleNode("spawn").Attributes["y"]?.InnerText);
                float shipSpawnZ = float.Parse(ship.SelectSingleNode("spawn").Attributes["z"]?.InnerText);

                bool shipCanMove = bool.Parse(ship.SelectSingleNode("canMove")?.Value);
                bool shipCanRotate = bool.Parse(ship.SelectSingleNode("canRotate")?.Value);

                
    
                GameObject tempTrial = new GameObject();
                tempTrial.AddComponent<Trial>();

                
            }
        }
        catch(XmlException e)
        {
            Debug.Log(e.GetType().ToString() + e.Message);
        }
        catch(IOException e)
        {
            Debug.Log(e.GetType().ToString() + e.Message);
        }

    }
	
	// Update is called once per frame
	void Update () {

        /// This is where the controller would read the XML data file
        /// Use a for loop to read the XML file and fill a queue of trial objects
        /// For the demo I will just be creating a trial case and hard coding the parameters 

        GameObject tempTrial = trial;

        // Load parameters into trial
        tempTrial.GetComponent<Trial>().shipSpawn = new Vector3(0f, -2.5f, 0f);
        tempTrial.GetComponent<Trial>().AsteroidMovementX = 1f;

        if (Input.GetKeyDown(KeyCode.Z) && trialStart != true)
        {
            /// If 'Z' is pressed, the engine spawns a new trial object
            /// The trial object will then take cover 

            trialStart = true;
            var createTrial = Instantiate(tempTrial, transform.position, transform.rotation);

            createTrial.transform.parent = gameObject.transform;
        }
    }

    void PauseGame() {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = true;
        } else
        {
            Time.timeScale = 0;
            isPaused = false;
        }
    }

    void OnGUI()
    {
        
    }
}


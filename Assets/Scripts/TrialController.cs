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

    private Queue<GameObject> trials = new Queue<GameObject>();

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

                // Ship
                float shipSpawnX = float.Parse(ship.SelectSingleNode("spawn").Attributes["x"].InnerText);
                float shipSpawnY = float.Parse(ship.SelectSingleNode("spawn").Attributes["y"].InnerText);
                float shipSpawnZ = float.Parse(ship.SelectSingleNode("spawn").Attributes["z"].InnerText);

                bool shipCanMove = bool.Parse(ship.Attributes["canMove"].Value);
                bool shipCanRotate = bool.Parse(ship.Attributes["canRotate"].Value);

                float shipMoveSpeed = float.Parse(ship.Attributes["moveSpeed"].Value);
                float shipRotSpeed = float.Parse(ship.Attributes["rotationSpeed"].Value);

                // Asteroid
                XmlNode spawnPoint = asteroid.SelectSingleNode("spawn");
                float asteroidSpawnX = float.Parse(spawnPoint.Attributes["x"].InnerText);
                float asteroidSpawnY = float.Parse(spawnPoint.Attributes["y"].InnerText);
                float asteroidSpawnZ = float.Parse(spawnPoint.Attributes["z"].InnerText);

                float asteroidMoveX = float.Parse(asteroid.Attributes["movementX"].Value);
                float asteroidMoveY = float.Parse(asteroid.Attributes["movementY"].Value);

                float asteroidRotSpeed = float.Parse(asteroid.Attributes["rotationSpeed"].Value);

                // Trial
                GameObject tempTrial = new GameObject();
                tempTrial.AddComponent<Trial>();

                // Trial Ship
                tempTrial.GetComponent<Trial>().shipSpawn = new Vector3(shipSpawnX, shipSpawnY, shipSpawnZ);

                tempTrial.GetComponent<Trial>().shipMove = shipCanMove;
                tempTrial.GetComponent<Trial>().shipRotate = shipCanRotate;

                tempTrial.GetComponent<Trial>().shipMoveSpeed = shipMoveSpeed;
                tempTrial.GetComponent<Trial>().shipRotateSpeed = shipRotSpeed;

                // Trial Asteroid
                tempTrial.GetComponent<Trial>().AsteroidSpawn = new Vector3(asteroidSpawnX, asteroidSpawnY, asteroidSpawnZ);

                tempTrial.GetComponent<Trial>().AsteroidMovementX = asteroidMoveX;
                tempTrial.GetComponent<Trial>().AsteroidMovementY = asteroidMoveY;

                tempTrial.GetComponent<Trial>().AsteroidRotation = asteroidRotSpeed;

                // Put trial into queue
                trials.Enqueue(tempTrial);


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

        if(trials.Count != 0)
        {
            GameObject currentTrial = trials.Dequeue();

            if (Input.GetKeyDown(KeyCode.Z) && trialStart != true)
            {
                /// If 'Z' is pressed, the engine spawns a new trial object
                /// The trial object will then take cover 

                trialStart = true;
                var createTrial = Instantiate(currentTrial, transform.position, transform.rotation);

                createTrial.transform.parent = gameObject.transform;
            }
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


using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class TrialController : MonoBehaviour {
    bool isPaused = false;

    public GameObject trial;

    public string stringToEdit = "Hello World";

    public bool trialStart = false;

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    //private Queue<GameObject> trials = new Queue<GameObject>();

    private int trialCount = 1;
    private int currentTrialCount = 1;

    

    // Use this for initialization
    void Start () {
         
        try{

            var doc = new XmlDocument();
            doc.Load(XML_FILE_PATH);
            XmlNodeList xmlTrials = doc.SelectNodes("/experiment/trial");
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
                

                // Trial Ship
                trial.GetComponent<Trial>().shipSpawn = new Vector3(shipSpawnX, shipSpawnY, shipSpawnZ);

                trial.GetComponent<Trial>().shipMove = shipCanMove;
                trial.GetComponent<Trial>().shipRotate = shipCanRotate;

                trial.GetComponent<Trial>().shipMoveSpeed = shipMoveSpeed;
                trial.GetComponent<Trial>().shipRotateSpeed = shipRotSpeed;

                // Trial Asteroid
                trial.GetComponent<Trial>().AsteroidSpawn = new Vector3(asteroidSpawnX, asteroidSpawnY, asteroidSpawnZ);

                trial.GetComponent<Trial>().AsteroidMovementX = asteroidMoveX;
                trial.GetComponent<Trial>().AsteroidMovementY = asteroidMoveY;

                trial.GetComponent<Trial>().AsteroidRotation = asteroidRotSpeed;

                GameObject tempTrial = GameObject.Instantiate(trial);

                tempTrial.SetActive(false);

                tempTrial.transform.parent = gameObject.transform;

                tempTrial.name = trialCount++.ToString();

                // Put trial into queue
                //trials.Enqueue(tempTrial);


            }

            changeTrialText(currentTrialCount);
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
        
        // "Press Z to start trial"
        if (Input.GetKeyDown(KeyCode.Z) && trialStart != true)
        {
            // If our current trial counter is less than or equal to our total trials
            if (currentTrialCount <= trialCount)
            {
                changeTrialText(currentTrialCount);

                toggleInstructionState();

                GameObject currentTrial = gameObject.transform.Find(currentTrialCount.ToString()).gameObject;

                /// If 'Z' is pressed, the engine spawns a new trial object
                /// The trial object will then take cover 

                trialStart = true;
                currentTrial.SetActive(true);

                currentTrialCount++;

                

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

    void changeTrialText(int num)
    {
        GameObject canvasTrialText = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Text trialText = canvasTrialText.GetComponent<Text>();

        trialText.text = "Trial " + num;
    }

    public void toggleInstructionState()
    {
        GameObject canvasInstruction = gameObject.transform.GetChild(0).GetChild(1).gameObject;

        canvasInstruction.SetActive(!canvasInstruction.activeSelf);
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Output;
using System;
using UnityEngine.SceneManagement;

public class TrialController : MonoBehaviour {
    /// 
    /// TrialController
    /// 
    /// Start():
    ///     Loads XML document
    ///     Grabs these loaded values and inserts them into TrialDataModel
    /// 
    /// Update():
    ///     Checks if 'Z' is pressed and a trial is not started yet
    ///         Dequeues TrialDataModel from TrialQueue
    ///         Toggles "Press 'Z' to start" GUI message
    ///         Loads Asteroid and Ship Prefabs based on dequeued TrialDataModel
    ///         Instantiates Ship and Asteroid into scene
    ///     
    ///     Checks if AsteroidDone is true
    ///         TrialStart becomes false
    ///         Destroys gameobjects with tags "Ship" and "Asteroid"
    ///         AsteroidDone becomes false
    ///         
    ///  trialPass():
    ///     Called from Asteroid Object

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    
    // list of data models to load the xml data into
    Queue<TrialDataModel> trialQueue = new Queue<TrialDataModel>();
    Queue<String> dataOutput = new Queue<String>();

    public GameObject shipPrefab;
    public GameObject asteroidPrefab;

    public GameObject MainMenu;

    public bool trialStart = false;
    public bool end = false;

    // Use this for initialization
    void Start () {

        Button menuBtn = MainMenu.GetComponent<Button>();
        menuBtn.onClick.AddListener(ClickMainMenu);

        try{
            int trialNumber = 1;

            var doc = new XmlDocument();
            doc.Load(XML_FILE_PATH);
            XmlNodeList xmlTrials = doc.SelectNodes("/experiment/trial");
            foreach(XmlNode xmlTrial in xmlTrials)
            {

                // LOAD XML DOCUMENT 
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

                // LOAD DATAMODELS WITH INFORMATION FROM XML
                // Trial
                TrialDataModel tempTrial = new TrialDataModel();

                // Trial Ship
                tempTrial.ShipSpawnX = shipSpawnX;
                tempTrial.ShipSpawnY = shipSpawnY;

                tempTrial.ShipMove = shipCanMove;
                tempTrial.ShipRotate = shipCanRotate;

                tempTrial.ShipMoveSpeed = shipMoveSpeed;
                tempTrial.ShipRotateSpeed = shipRotSpeed;

                // Trial Asteroid
                tempTrial.AsteroidSpawnX = asteroidSpawnX;
                tempTrial.AsteroidSpawnY = asteroidSpawnY;

                tempTrial.AsteroidMovementX = asteroidMoveX;
                tempTrial.AsteroidMovementY = asteroidMoveY;

                tempTrial.AsteroidRotation = asteroidRotSpeed;

                // Trial Specific Information
                tempTrial.TrialID = trialNumber;
                trialNumber++;

                // Add TrialDataModel To List
                trialQueue.Enqueue(tempTrial);
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

    // Our current trial
    TrialDataModel trialModel = new TrialDataModel();
    

    void Update () {
        try
        {
            // "Press Z to start trial"
            if (Input.GetKeyDown(KeyCode.Z) && trialStart != true && end == false)
            {
                trialStart = true;
                trialModel = trialQueue.Dequeue();
                toggleInstructionState();

                // Load Asteroid 
                asteroidPrefab.GetComponent<Asteroid>().rotation = true;
                asteroidPrefab.GetComponent<Asteroid>().rotationSpeed = trialModel.AsteroidRotation;
                asteroidPrefab.GetComponent<Asteroid>().movementSpeedX = trialModel.AsteroidMovementX;
                asteroidPrefab.GetComponent<Asteroid>().movementSpeedY = trialModel.AsteroidMovementY;


                // Load Ship
                shipPrefab.GetComponent<PlayerMovement>().canMove = trialModel.ShipMove;
                shipPrefab.GetComponent<PlayerMovement>().canRotate = trialModel.ShipRotate;
                shipPrefab.GetComponent<PlayerMovement>().maxSpeed = trialModel.ShipMoveSpeed;
                shipPrefab.GetComponent<PlayerMovement>().rotSpeed = trialModel.ShipRotateSpeed;

                //Instantiate GameObjects
                Vector3 asteroidSpawnVector = new Vector3(trialModel.AsteroidSpawnX, trialModel.AsteroidSpawnY, 0);

                var createAsteroid = Instantiate(asteroidPrefab, asteroidSpawnVector, transform.rotation);
                createAsteroid.transform.parent = gameObject.transform;

                Vector3 shipSpawnVector = new Vector3(trialModel.ShipSpawnX, trialModel.ShipSpawnY, 0);

                var createShip = Instantiate(shipPrefab, shipSpawnVector, transform.rotation);
                createShip.transform.parent = gameObject.transform;
            }
        } catch(Exception e)
        {
            end = true;
            GameObject canvasInstruction = gameObject.transform.GetChild(0).GetChild(1).gameObject;

            //canvasInstruction.transform.GetChild(1).GetComponent<Text>().text = "No More Trials";

            MainMenu.SetActive(true);
            
        }
        

    }

    public void trialPass(int passID, bool hit, float totalPassTime)
    {
        bool wasFired = GameObject.FindWithTag("Ship").GetComponent<PlayerMovement>().wasFired;
        float fireTime = GameObject.FindWithTag("Ship").GetComponent<PlayerMovement>().timeFired;

        trialModel.addPass(passID, wasFired, hit, fireTime, totalPassTime);

        GameObject.FindWithTag("Ship").GetComponent<PlayerMovement>().wasFired = false;
        //GameObject.FindWithTag("Ship").GetComponent<PlayerMovement>().timeFired = 0f;

        if (hit)
        {
            trialStart = false;
            toggleInstructionState();

            Destroy(GameObject.FindWithTag("Ship"));
            Destroy(GameObject.FindWithTag("Asteroid"));

            dataCollection();
        }
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void toggleInstructionState()
    {
        GameObject canvasInstruction = gameObject.transform.GetChild(0).GetChild(1).gameObject;

        canvasInstruction.SetActive(!canvasInstruction.activeSelf);
    }

    // Builds the strings to send to the .CSV file
    void dataCollection()
    {
        String output = "";

        // Output Format (for now):
        // TrialID, Passes, (PassID, If Proj Fired, If Proj Hit, ProjFireTime, PassTotalTime)
        output += trialModel.TrialID + ", ";
        output += trialModel.TotalNumPasses;
        output += trialModel.returnPassData();

        Debug.Log(output);
    }
}



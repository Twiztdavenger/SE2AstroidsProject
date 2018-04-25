using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Output;
using System;
using UnityEngine.SceneManagement;

public class TrialMenu : MonoBehaviour {

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    // list of data models to load the xml data into
    Queue<TrialDataModel> trialQueue = new Queue<TrialDataModel>();

    public Text trialsText;
    private String trialsContent = "";

    public Button loadData;
    public Button startExperiment;

    private int testNum = 98;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
        //Button for LoadData
        Button btn = loadData.GetComponent<Button>();
        btn.onClick.AddListener(LoadXML);

        btn = startExperiment.GetComponent<Button>();
        btn.onClick.AddListener(StartExperiment);

    }

    void LoadXML()
    {
        try
        {
            int trialNumber = 1;

            var doc = new XmlDocument();
            doc.Load(XML_FILE_PATH);
            XmlNodeList xmlTrials = doc.SelectNodes("/experiment/trial");
            foreach (XmlNode xmlTrial in xmlTrials)
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

                trialsContent += xmlTrial.Attributes["name"].Value + "\n";
            }
        }
        catch (XmlException e)
        {
            Debug.Log(e.GetType().ToString() + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log(e.GetType().ToString() + e.Message);
        }

        trialsText.text = trialsContent;
    }

    void StartExperiment()
    {
        SceneManager.LoadScene("Client");
    }

    // Update is called once per frame
    void Update () {
		
	}
}

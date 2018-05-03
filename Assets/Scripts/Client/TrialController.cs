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

    public GameObject endWindow;

    public GameObject canvasText;
    public GameObject trialText;

    public bool trialStart = false;
    public bool endOfExperiment = false;

    //BUTTONS FOR ENDTRIAL WINDOW
    public Button MainMenu;
    public Button Restart;

    // Use this for initialization
    void Start () {
        trialQueue = xmlData.TrialQueue;
        endWindow.SetActive(false);

        Button btn = MainMenu.GetComponent<Button>();
        btn.onClick.AddListener(GoToMainMenu);

        btn = Restart.GetComponent<Button>();
        btn.onClick.AddListener(restartTrials);

        trialStart = false;
        endOfExperiment = false;

        trialText.GetComponent<Text>().text = "Trial " + trialQueue.Count.ToString();

        int width = Screen.width;
        Camera.main.aspect = width / Screen.height;

    }

    // Our current trial
    TrialDataModel trialModel = new TrialDataModel();
    

    void Update () {
        try
        {
            if(trialQueue.Count == 0 && !endOfExperiment && !trialStart)
            {
                endOfExperiment = true;
                trialStart = false;
                changeCanvasText("RESET");

                endWindow.SetActive(true);
                Debug.Log(outputData.getTrialOutput());

            }
            // "Press Z to start trial"
            else if (Input.GetKeyDown(KeyCode.Z) == true && !trialStart && !endOfExperiment)
            {
                trialStart = true;
                trialModel = trialQueue.Dequeue();

                

                changeCanvasText("RESET");

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
            Debug.Log(outputData.getTrialOutput());

            Debug.Log(e);
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
            changeCanvasText("PRETRIAL");
            trialStart = false;

            Destroy(GameObject.FindWithTag("Ship"));
            Destroy(GameObject.FindWithTag("Asteroid"));

            trialText.GetComponent<Text>().text = "Trial " + trialQueue.Count;

            dataCollection();
        }
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void changeCanvasText(string gameState)
    {
        string content = "";

        switch(gameState)
        {
            case "PRETRIAL":
                content = "Press 'Z' to Start Trial";
                break;
            case "MISS":
                content = "Miss!";
                break;
            case "HIT":
                content = "You Did It!";
                break;
            case "END":
                content = "End of Trials";
                break;
            case "RESET":
                content = "";
                break;
        }

        canvasText.GetComponent<Text>().text = content;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void restartTrials()
    {
        SceneManager.LoadScene("Client");
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

        outputData.finTrialQueue.Enqueue(trialModel);

        Debug.Log(output);
    }
}



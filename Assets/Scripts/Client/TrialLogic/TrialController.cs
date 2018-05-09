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
    Queue<String> dataOutputQueue = new Queue<String>();

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

    public Vector2 minProjCoordinates = new Vector2();
    public Vector2 minAstCoordinates = new Vector2();

    //OBSERVER PATTERN

    private static TrialController _instance;
    public static TrialController Instance;

    public delegate void TrialStages(TrialDataModel data);
    public static event TrialStages BeginTrial;
    public static event TrialStages PracticeTrial;

    public delegate void TrialUI();
    public static event TrialUI StartTrial;

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        trialQueue = xmlData.TrialQueue;
        endWindow.SetActive(false);

        Button btn = MainMenu.GetComponent<Button>();
        btn.onClick.AddListener(GoToMainMenu);

        //btn = Restart.GetComponent<Button>();
        //btn.onClick.AddListener(restartTrials);

        trialStart = false;
        endOfExperiment = false;

        trialText.GetComponent<Text>().text = "Trial " + trialQueue.Count.ToString();
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
                //changeCanvasText("RESET");

                endWindow.SetActive(true);

                outputData.getTrialOutput();

            }
            // "Press Z to start trial"
            else if (Input.GetKeyDown(KeyCode.Z) == true && !trialStart && !endOfExperiment)
            {
                trialStart = true;
                trialModel = trialQueue.Dequeue();

                BeginTrial(trialModel);
                StartTrial();
            }
        } catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    public void trialPass(int passID, bool hit, float totalPassTime)
    {
        bool wasFired = false;

        float fireTime = GameObject.FindWithTag("Ship").GetComponent<PlayerMovement>().timeFired;

        Debug.Log(DistanceInfo.projMinX);
        Debug.Log(DistanceInfo.projMinY);

        float pX = DistanceInfo.projMinX;
        float pY = DistanceInfo.projMinY;

        float aX = DistanceInfo.astMinX;
        float aY = DistanceInfo.astMinY;

        trialModel.addPass(passID, wasFired, hit, fireTime, totalPassTime, pX, pY, aX, aY);

        minProjCoordinates = Vector2.zero;
        minAstCoordinates = Vector2.zero;

        GameObject.FindWithTag("Ship").GetComponent<PlayerMovement>().timeFired = 0f;

        if (hit)
        {
            //changeCanvasText("PRETRIAL");
            trialStart = false;

            Destroy(GameObject.FindWithTag("Ship"));
            Destroy(GameObject.FindWithTag("Asteroid"));

            trialText.GetComponent<Text>().text = "Trial " + trialModel.TrialID.ToString();

            dataCollection();
        }
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void restartTrials()
    {
        SceneManager.LoadScene("Client");
    }

    public void LogCoord(Vector2 minProj, Vector2 minAst)
    {
        minProjCoordinates = minProj;
        minAstCoordinates = minAst;
    }
    // Builds the strings to send to the .CSV file
    void dataCollection()
    {
        OutputTrialModel tempOutputModel = new OutputTrialModel();

        tempOutputModel.TrialID = trialModel.TrialID;
        tempOutputModel.ExperimentName = "Experiment Data";
        tempOutputModel.PracticeRound = false;
        tempOutputModel.TotalNumPasses = trialModel.TotalNumPasses;
        tempOutputModel.DelayTime = trialModel.SpawnDelayTime;

        string passData = trialModel.returnPassData();

        tempOutputModel.passData = passData;


        outputData.OutputTrialQueue.Enqueue(tempOutputModel);
    }
}



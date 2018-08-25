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

    public bool trialRunning = false;
    public bool onLastTrial = false;

    //BUTTONS FOR ENDTRIAL WINDOW
    public Button MainMenu;
    public Button Restart;

    public Vector2 minProjCoordinates = new Vector2();
    public Vector2 minAstCoordinates = new Vector2();

    //OBSERVER PATTERN
    private static TrialController _instance;
    public static TrialController Instance;

    public delegate void TrialStagesNextTrial(TrialDataModel data);
    public static event TrialStagesNextTrial BeginNextTrial;

    public delegate void TrialUI();
    public static event TrialUI BeginNextTrialUI;

    public delegate void TrialStagesEndTrials();
    public static event TrialStagesEndTrials EndExperiment;

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        trialQueue = InputDataHolder.TrialQueue;
        endWindow.SetActive(false);

        Button btn = MainMenu.GetComponent<Button>();
        btn.onClick.AddListener(GoToMainMenu);

        trialRunning= false;
        onLastTrial = false;

        Instructions.ReadyForNextTrial += onNextTrial;
    }

    // Our current trial
    TrialDataModel trialModel = new TrialDataModel();
    private string trialName = "";
    

    void Update () {
        try
        {
            //If there  is nothing in the queue, we arent 
            if(trialQueue.Count == 0 && !onLastTrial && !trialRunning)
            {
                EndExperiment();
                trialRunning = false;

                trialName = "";

                endWindow.SetActive(true);

                //outputData.getTrialOutput();

            }
            // "Press Z to start trial"
            else if (Input.GetKeyDown(KeyCode.Z) == true && !trialRunning)
            {
                if(onLastTrial)
                {
                    onLastTrial = false;
                }
                trialRunning = true;

                Debug.Log(trialModel.TrialName);

                BeginNextTrial(trialModel);
                BeginNextTrialUI();
            }
        } catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    void onNextTrial()
    {
        try
        {
            if (trialQueue.Count == 1)
            {
                onLastTrial = true;
            }
            trialRunning = false;
            trialModel = trialQueue.Dequeue();
            trialName = trialModel.TrialName;

            trialText.GetComponent<Text>().text = trialName;
        } catch(Exception e)
        {
            Debug.Log(e);
        }
        
    }

    public void trialPass(int passID, bool hit, float totalPassTime)
    {
        bool wasFired = false;

        float fireTime = GameObject.FindWithTag("Ship").GetComponent<Ship>().timeFired;

        Debug.Log(DistanceInfo.projMinX);
        Debug.Log(DistanceInfo.projMinY);

        float pX = DistanceInfo.projMinX;
        float pY = DistanceInfo.projMinY;

        float aX = DistanceInfo.astMinX;
        float aY = DistanceInfo.astMinY;

        trialModel.addPass(passID, wasFired, hit, fireTime, totalPassTime, pX, pY, aX, aY);

        minProjCoordinates = Vector2.zero;
        minAstCoordinates = Vector2.zero;

        GameObject.FindWithTag("Ship").GetComponent<Ship>().timeFired = 0f;

        if (hit)
        {
            trialRunning = false;

            Destroy(GameObject.FindWithTag("Ship"));
            Destroy(GameObject.FindWithTag("Asteroid"));

            //trialText.GetComponent<Text>().text = "Trial " + trialModel.TrialID;


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
        //OutputTrialModel tempOutputModel = new OutputTrialModel();


        //tempOutputModel.TrialID = trialModel.TrialID;
        //tempOutputModel.ExperimentName = "Experiment Data";
        //tempOutputModel.PracticeRound = false;
        //tempOutputModel.TotalNumPasses = trialModel.TotalNumPasses;
        //tempOutputModel.DelayTime = trialModel.SpawnDelayTime;

        //tempOutputModel.TrialID = trialModel.TrialID;
        //tempOutputModel.ExperimentName = "Experiment Data";
        //tempOutputModel.PracticeRound = false;
        //tempOutputModel.TotalNumPasses = trialModel.TotalNumPasses;
        //tempOutputModel.DelayTime = trialModel.SpawnDelayTime;

        //string passData = trialModel.returnPassData();

        //tempOutputModel.passData = passData;


        //outputData.OutputTrialQueue.Enqueue(tempOutputModel);
    }

    private void OnDisable()
    {
        Instructions.ReadyForNextTrial -= onNextTrial;
    }
}



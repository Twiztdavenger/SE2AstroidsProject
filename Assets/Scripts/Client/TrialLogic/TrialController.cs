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

    public GameObject endWindow;

    public GameObject canvasText;
    public GameObject trialText;

    public GameObject TrialOutputDataCollector;

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
    public static event TrialStagesNextTrial BeginTrial;

    public delegate void TrialUI();
    public static event TrialUI BeginNextTrialUI;

    public delegate void TrialStages();
    //public static event TrialStages BeginTrial;
    public static event TrialStages EndExperiment;

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
    private string experimentName = "";
    

    void Update () {
            //FIX: THIS IF CONDITIONAL STATEMENT IS ALWAYS LOOPING DUE TO ALL CONDITIONS BEING MET AT END OF TRIALS
            //THIS IS REALLY BAD I SHOULD REALLY FIX THIS
            if(trialQueue.Count == 0 && !onLastTrial && !trialRunning)
            {
                EndExperiment();
                trialRunning = false;
                trialName = "";

                endWindow.SetActive(true);
            }
            // "Press Z to start trial"
            else if (Input.GetKeyDown(KeyCode.Z) == true && !trialRunning)
            {
                if(onLastTrial)
                {
                    onLastTrial = false;
                }
                trialRunning = true;

                BeginTrial(trialModel);
                var createTrialOutputDataCollector = Instantiate(TrialOutputDataCollector);

                GameObject.FindGameObjectWithTag("TrialOutputDataCollector").GetComponent<TrialOutputDataCollector>().setTrialData(1, trialModel.TrialName, "Testing Output");
                //BeginNextTrialUI();
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



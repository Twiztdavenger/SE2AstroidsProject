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
    
    /// <summary>
    /// 
    /// </summary>

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";


    // list of data models to load the xml data into
    Queue<TrialDataModel> trialQueue = new Queue<TrialDataModel>();
    Queue<String> dataOutputQueue = new Queue<String>();

    public int trialCount = 0;

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

        trialCount = trialQueue.Count;

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
            if(trialCount == 0 && !trialRunning)
            {
                EndExperiment();
                trialName = "";
                
            }
            // "Press Z to start trial"
            else if (Input.GetKeyDown(KeyCode.Z) == true && !trialRunning)
            {
                trialCount--;
                trialRunning = true;

                BeginTrial(trialModel);
                var createTrialOutputDataCollector = Instantiate(TrialOutputDataCollector);

                float asteroidSlope = trialModel.AsteroidMovementY / trialModel.AsteroidMovementX;

                GameObject.FindGameObjectWithTag("TrialOutputDataCollector").GetComponent<TrialOutputDataCollector>().setTrialData(trialModel.TrialID, trialModel.TrialName, "Testing Output", trialModel.ParticipantID, trialModel.AsteroidMovementX, asteroidSlope);
            }
        
    }

    void onNextTrial()
    {
        try
        {
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
    private void OnDestroy()
    {
        Instructions.ReadyForNextTrial -= onNextTrial;
    }
}



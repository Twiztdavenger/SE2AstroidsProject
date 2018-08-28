using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Output;
using System;
using UnityEngine.SceneManagement;

public class TrialMenu : MonoBehaviour
{

    private const string XML_FILE_PATH = @"Assets/Data/Experiment.xml";

    // list of data models to load the xml data into
    Queue<TrialDataModel> trialQueue = new Queue<TrialDataModel>();

    private String trialsContent = "";

    public GameObject trialsText;

    public Button startExperiment;
    public Button quit;

    public Button nextWindow;
    public Button back;

    public GameObject PartIDWindow;
    public GameObject AccessCodeWindow;

    public Text PartIDText;
    public Text AccessCodeText;

    public GameObject httpServer;
    

    //public Button;

    // Use this for initialization
    void Start()
    {
        //Button for LoadData

        Button btn = quit.GetComponent<Button>();
        btn.onClick.AddListener(QuitExperiment);

        btn = startExperiment.GetComponent<Button>();
        btn.onClick.AddListener(StartExperiment);

        btn = nextWindow.GetComponent<Button>();
        btn.onClick.AddListener(EnterPartID);
    }

    

    // Boolean to check if coroutine is still running
    private bool startCo = true;

    void EnterPartID()
    {
        StartCoroutine("WindowTransition");
        
    }

    

    void LoadXML()
    {
        try
        {
            trialQueue = InputDataHolder.TrialQueue;

            var doc = new XmlDocument();
            doc.Load(XML_FILE_PATH);
            XmlNodeList xmlTrials = doc.SelectNodes("/experiment/trial");
            foreach (XmlNode xmlTrial in xmlTrials)
            {
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

        trialsText.GetComponent<Text>().text = trialsContent;
    }

    void StartExperiment()
    {
        int accessCode;
        if (int.TryParse(trialsText.GetComponent<Text>().text, out accessCode))
        {
            StartCoroutine(httpServer.GetComponent<HTTPServer>().GetText(accessCode, () => {
                if(httpServer.GetComponent<HTTPServer>().invalidAccessCode == false)
                {
                    SceneManager.LoadScene("Client");
                } else
                {
                    Debug.Log("That was an invalid access code");
                }
                
            }));
            //Debug.Log("Data for Access code: " + accessCode + response.ToString());
        }
    }


    
    void QuitExperiment()
    {
        Application.Quit();
    }
}

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
            trialQueue = xmlData.TrialQueue;

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

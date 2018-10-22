using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class OutputManager : MonoBehaviour {

    public static Queue<OutputTrialModel> OutputTrialQueue = new Queue<OutputTrialModel>();

    private static List<string[]> rowData = new List<string[]>();
    public static string _CSV_DATA_PATH_ = "";

    public static string participantID = "";
    public static int trialID;

    public delegate void OutputEvent(string path);
    public static event OutputEvent DoneWritingData;

	// Use this for initialization

    //TODO: Keep track of participant ID 
	void Start () {
        TrialOutputDataCollector.ReturnTrialOutputData += AddTrialModel;
        TrialController.EndExperiment += getTrialOutput;

        _CSV_DATA_PATH_ = Application.dataPath;
    }

    void AddTrialModel(OutputTrialModel output)
    {
        OutputTrialQueue.Enqueue(output);
        Debug.Log("Added Trial: " + output.TrialID + " " + output.TrialName);
    }

    static public void getTrialOutput()
    {
        if(OutputTrialQueue.Count > 0)
        {
            participantID = OutputTrialQueue.Peek().ParticipantID;
            trialID = OutputTrialQueue.Peek().TrialID;
            TrialController.EndExperiment -= getTrialOutput;
            string[] rowDataTemp = new string[4];
            rowDataTemp[0] = "Trial Name";
            rowDataTemp[1] = "Asteroid Speed (grid unit per second on x axis)";
            rowDataTemp[2] = "Asteroid Slope";
            rowDataTemp[3] = "Pass Data (PassID; If Player Fired; Time Player Fired; If Asteroid Hit; Min Distance Proj and Asteroid)";

            rowData.Add(rowDataTemp);
            while (OutputTrialQueue.Count > 0)
            {
                OutputTrialModel tempTrial = OutputTrialQueue.Dequeue();

                rowDataTemp = new string[4];
                rowDataTemp[0] = tempTrial.TrialName.ToString();
                rowDataTemp[1] = tempTrial.AsteroidSpeed.ToString();
                rowDataTemp[2] = tempTrial.AsteroidSlope.ToString();
                rowDataTemp[3] = tempTrial.returnPassData();
                rowData.Add(rowDataTemp);
            }

            string[][] output = new string[rowData.Count][];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = rowData[i];
            }

            int length = output.GetLength(0);
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));

            DateTime today = DateTime.Now;

            _CSV_DATA_PATH_ += "/OutputData/" + "/Participant_" + participantID + "_" + today.ToString("MM.dd.yyyy");
            Directory.CreateDirectory(_CSV_DATA_PATH_);
            string _CSV_DATA_PATH_FINAL = _CSV_DATA_PATH_ + "/Experiment" + trialID.ToString() + "_1" + ".csv";

            if (File.Exists(_CSV_DATA_PATH_FINAL))
            {
                bool ifFileExists = true;
                int fileIdentifier = 1;
                while(ifFileExists)
                {
                    fileIdentifier++;
                    _CSV_DATA_PATH_FINAL = _CSV_DATA_PATH_ + "/Experiment" + trialID.ToString() + "_" + fileIdentifier + ".csv";
                    ifFileExists = false;
                }
            }

            StreamWriter outStream = System.IO.File.CreateText(_CSV_DATA_PATH_FINAL);
            outStream.WriteLine(sb);
            outStream.Close();
            DoneWritingData(_CSV_DATA_PATH_FINAL);

        } else
        {
            Debug.Log("No trial data to output");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

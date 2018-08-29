using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class OutputManager : MonoBehaviour {

    public static Queue<OutputTrialModel> OutputTrialQueue = new Queue<OutputTrialModel>();

    private static List<string[]> rowData = new List<string[]>();
    public static string _CSV_DATA_PATH_ = @"Assets/Output";

    public static string participantID = "";

	// Use this for initialization

    //TODO: Keep track of participant ID 
	void Start () {
        TrialOutputDataCollector.ReturnTrialOutputData += AddTrialModel;
        TrialController.EndExperiment += getTrialOutput;
	}

    void AddTrialModel(OutputTrialModel output)
    {
        OutputTrialQueue.Enqueue(output);
        Debug.Log("Added Trial: " + output.TrialID + " " + output.TrialName);
    }

    void populatePartID(string partID)
    {
        participantID = partID;
    }
    

    static public void getTrialOutput()
    {
        if(OutputTrialQueue.Count > 0)
        {
            participantID = OutputTrialQueue.Peek().ParticipantID;
            TrialController.EndExperiment -= getTrialOutput;
            string[] rowDataTemp = new string[4];
            rowDataTemp[0] = "Experiment Name";
            rowDataTemp[1] = "Trial Id";
            rowDataTemp[2] = "Trial Name";
            rowDataTemp[3] = "Pass Data";

            rowData.Add(rowDataTemp);
            while (OutputTrialQueue.Count > 0)
            {
                OutputTrialModel tempTrial = OutputTrialQueue.Dequeue();

                rowDataTemp = new string[4];
                rowDataTemp[0] = "Experiment";
                rowDataTemp[1] = tempTrial.TrialID.ToString();
                rowDataTemp[2] = tempTrial.TrialName.ToString();
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

            _CSV_DATA_PATH_ = @"Assets/Output" + "/CSV/" + "Participant_" + participantID + "_Output" + ".csv";

            StreamWriter outStream = System.IO.File.CreateText(_CSV_DATA_PATH_);
            outStream.WriteLine(sb);
            outStream.Close();
        } else
        {
            Debug.Log("No trial data to output");
        }
    }




    // Update is called once per frame
    void Update () {
		
	}
}

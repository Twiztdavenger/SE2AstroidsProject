using Assets.Scripts.Output;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class outputData
{
    public static Queue<OutputTrialModel> OutputTrialQueue = new Queue<OutputTrialModel>();

    private static List<string[]> rowData = new List<string[]>();

    public static string _CSV_DATA_PATH_ = @"Assets/Output";

    public static Queue<OutputTrialModel> TrialQueue
    {
        set
        {
            OutputTrialQueue = value;
        }
    }

    static public void getTrialOutput()
    {
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Experiment Name";
        rowDataTemp[1] = "Trial Id";
        rowDataTemp[2] = "Practice Round";
        rowDataTemp[3] = "Pass Count";
        rowDataTemp[4] = "Spawn Delay Time";
        rowDataTemp[5] = "passData";

        rowData.Add(rowDataTemp);
        // TODO: Work on finding a way to output all trial data at once
        while (OutputTrialQueue.Count > 0)
        {
            OutputTrialModel tempTrial = OutputTrialQueue.Dequeue();

            rowDataTemp = new string[6];
            rowDataTemp[0] = tempTrial.ExperimentName;
            rowDataTemp[1] = tempTrial.TrialID.ToString(); // ID
            rowDataTemp[2] = tempTrial.PracticeRound.ToString();
            rowDataTemp[3] = tempTrial.TotalNumPasses.ToString(); // ID
            rowDataTemp[4] = tempTrial.DelayTime.ToString(); // ID
            rowDataTemp[5] = tempTrial.passData; // ID
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

        _CSV_DATA_PATH_ = @"Assets/Output" + "/CSV/" + "Experiment_Output.csv";

        StreamWriter outStream = System.IO.File.CreateText(_CSV_DATA_PATH_);
        outStream.WriteLine(sb);
        outStream.Close();

    }

}



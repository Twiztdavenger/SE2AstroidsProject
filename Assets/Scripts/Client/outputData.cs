using Assets.Scripts.Output;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class outputData {
    public static Queue<TrialDataModel> finTrialQueue = new Queue<TrialDataModel>();

    private static List<string[]> rowData = new List<string[]>();

    public static Queue<TrialDataModel> TrialQueue
    {
        set
        {
            finTrialQueue = value;
        }
    }

    static public StringBuilder getTrialOutput()
    {
        string[] rowDataTemp = new string[3];
        rowDataTemp[0] = "Trial ID";
        rowDataTemp[1] = "Pass Data";

        rowData.Add(rowDataTemp);


        // TODO: Work on finding a way to output all trial data at once
        while(finTrialQueue.Count > 0)
        {
            TrialDataModel tempTrial = finTrialQueue.Dequeue();

            rowDataTemp = new string[2];
            rowDataTemp[0] = tempTrial.TrialID.ToString(); // ID
            rowDataTemp[1] = tempTrial.returnPassData(); // Passdata
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

        return sb;
    }

}

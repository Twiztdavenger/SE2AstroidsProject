using System.Collections;
using System.Collections.Generic;


public class OutputTrialModel{
    public int TrialID { get; set; }
    public string ParticipantID { get; set; }
    public string TrialName { get; set; }
    public float AsteroidSlope { get; set; }
    public float AsteroidSpeed { get; set; }
    public bool PracticeRound { get; set; } //Not implemented yet
    public int TotalNumPasses { get; set; } //Not implemented yet
    public float DelayTime { get; set; } //Not implemented yet

    public ArrayList OutputPassDataList { get; set; }

    public string returnPassData()
    {
        string passOutput = "";
        if(OutputPassDataList.Count > 0)
        {
            foreach (OutputPassModel pass in OutputPassDataList)
            {
                passOutput += pass.ToString() + " ";
            }
        } else
        {
            passOutput = "No Pass Data";
        }
        
        return passOutput;
    }
}

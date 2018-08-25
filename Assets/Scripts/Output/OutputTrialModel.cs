using System.Collections;
using System.Collections.Generic;


public class OutputTrialModel{
    public int TrialID { get; set; }
    public string ExperimentName { get; set; }
    public bool PracticeRound { get; set; }
    public int TotalNumPasses { get; set; }
    public float DelayTime { get; set; }

    public string passData { get; set; }

    public ArrayList OutputPassDataList { get; set; }
}

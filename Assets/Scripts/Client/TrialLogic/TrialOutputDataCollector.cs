using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialOutputDataCollector : MonoBehaviour {
    private int PassNumID = 0;

    //Trial Data
    private bool PracticeRound; //Not Implemented Yet
    private int TotalNumPasses; //Not Implemented Yet
    private float DelayTime; //Not Implemented Yet

    private float AsteroidSpeed;

    private bool canAddPass = true;

    public OutputTrialModel TrialOutputModel;

    public ArrayList PassOutputDataList = new ArrayList();

    public delegate void TrialOutputReturnData(OutputTrialModel output);
    public static event TrialOutputReturnData ReturnTrialOutputData;

    // Use this for initialization
    void Start()
    {
        PassOutputDataCollector.ReturnPassOutputData += addPassOutputDataModel;
        PassManager.EndOfTrial += addPassListToTrial;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void addPassOutputDataModel(bool ifShipFired, bool ifAsteroidHit, float timePlayerShotInSeconds, float projAsteroidMinDistance)
    {
        if(canAddPass)
        {
            PassNumID++;
            OutputPassModel temp = new OutputPassModel()
            {
                PassID = PassNumID,
                IfShipFired = ifShipFired,
                TimePlayerShotInSeconds = timePlayerShotInSeconds,
                ProjAsteroidMinDistance = projAsteroidMinDistance,
                IfAsteroidWasHit = ifAsteroidHit
            };
            //Debug.Log("Adding pass data: " + PassNumID + " " + ifShipFired + " " + timePlayerShotInSeconds + " " + projAsteroidMinDistance);
            PassOutputDataList.Add(temp);
        }
        
    }

    void addPassListToTrial()
    {
        TrialOutputModel.OutputPassDataList = PassOutputDataList;
        //Debug.Log("How many passes were in this trial: " + PassOutputDataList.Count);
    }

    public void setTrialData(int trialID, string trialName, string experimentName, string partID, float asteroidSpeed, float asteroidSlope)
    {
        OutputTrialModel temp = new OutputTrialModel
        {
            TrialID = trialID,
            TrialName = trialName,
            ParticipantID = partID,
            AsteroidSpeed = asteroidSpeed,
            AsteroidSlope = asteroidSlope
            //ExperimentName = experimentName,
        };

        TrialOutputModel = temp;
    }

    private void OnDestroy()
    {
        ReturnTrialOutputData(TrialOutputModel);
        PassOutputDataCollector.ReturnPassOutputData -= addPassOutputDataModel;
        PassManager.EndOfTrial -= addPassListToTrial;
        canAddPass = false;
    }

}

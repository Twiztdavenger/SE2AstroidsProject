using Assets.Scripts.Output;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class xmlData  {
    public static Queue<TrialDataModel> trialQueue = new Queue<TrialDataModel>();

    public static Queue<TrialDataModel> TrialQueue
    {
        get
        {
            return trialQueue;
        }
        set
        {
            trialQueue = value;
        }
    }

}

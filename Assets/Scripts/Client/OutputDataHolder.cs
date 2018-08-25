using Assets.Scripts.Output;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OutputDataHolder
{
    public static ArrayList outputTrialList = new ArrayList();

    public static ArrayList OutputTrialList
    {
        get
        {
            return outputTrialList;
        }
        set
        {
            outputTrialList = value;
        }
    }

}

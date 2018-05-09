using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

    public Text iText;

    private float timer = 60f;

    private void Awake()
    {
        if(iText == null)
        {
            Debug.Log("Something Went Wrong");
        }
    }

    // Use this for initialization
    void Start () {
        TrialController.StartTrial += onBeginTrial;
	}

    void onBeginTrial()
    {
        iText.text = "";
    }

    void onPracticeTrial()
    {
        if (iText != null)
        {
            iText.text = "Practice Trial";
        }
        else
        {
            Debug.Log("Something went wrong");
        }

    }

    void onEndTrial()
    {
        iText.text = "End of Trial";
    }
	
	// Update is called once per frame
	void Update () {
	}
}

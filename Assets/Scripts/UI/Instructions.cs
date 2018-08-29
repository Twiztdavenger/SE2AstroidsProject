using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

    private float timer = 60f;

    public delegate void TrialUIEvents();
    public static event TrialUIEvents ReadyForNextTrial;
    public static event TrialUIEvents BeginTrial;
    public static event TrialUIEvents EndOfTrials;
    public static event TrialUIEvents CountdownOver;

    

    public GameObject endWindow;

    public float time = 3f;
    private int countdownTime = 3;

    private void Awake()
    {
        if(this.gameObject.GetComponent<Text>() == null)
        {
            Debug.Log("Something Went Wrong");
        }
    }

    // Use this for initialization
    void Start () {
        PassManager.BeginPass += onBeginPass;
        TrialController.EndExperiment += onEndExperiment;
        Asteroid.EndOfPass += EndOfPassMessage;
        ReadyForNextTrial();
	}

    void onBeginPass()
    {
        this.gameObject.GetComponent<Text>().text = "";
        StartCoroutine("PassCountdown");
    }

    void onPracticeTrial()
    {
        this.gameObject.GetComponent<Text>();
    }

    void onEndExperiment()
    {
        this.gameObject.GetComponent<Text>().text = "No More Trials";
        StartCoroutine("EndWindow");
    }

    void EndOfPassMessage(bool wasAsteroidHit)
    {
        if(wasAsteroidHit)
        {
            this.gameObject.GetComponent<Text>().text = "Hit!";
            StartCoroutine("NextTrial");
        } else
        {
            this.gameObject.GetComponent<Text>().text = "Miss!";
            StartCoroutine("MissNextPassMessage");
        }
    }

    IEnumerator PassCountdown()
    {
        this.gameObject.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<Text>().text = "";
        CountdownOver();
    }

    IEnumerator EndMessage()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Text>().text = "End of Trials";
    }

    IEnumerator MissNextPassMessage()
    {
        yield return new WaitForSeconds(1f);
        onBeginPass();
    }

    IEnumerator NextTrial()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Text>().text = "Press 'Z' to start next trial";
        ReadyForNextTrial();
    }

    IEnumerator EndWindow()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Text>().text = "";
        endWindow.SetActive(true);
    }

    private void OnDestroy()
    {
        PassManager.BeginPass -= onBeginPass;
        TrialController.EndExperiment -= onEndExperiment;
        Asteroid.EndOfPass -= EndOfPassMessage;
    }
}

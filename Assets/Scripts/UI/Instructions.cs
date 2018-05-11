using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

    private float timer = 60f;

    public delegate void TrialUIEvents();
    public static event TrialUIEvents ReadyForNextTrial;
    public static event TrialUIEvents EndOfTrials;

    public GameObject endWindow;

    private void Awake()
    {
        if(this.gameObject.GetComponent<Text>() == null)
        {
            Debug.Log("Something Went Wrong");
        }
    }

    // Use this for initialization
    void Start () {
        TrialController.StartTrial += onBeginTrial;
        TrialController.TrialsEnd += onEndTrials;
        Asteroid.Hit += HitMessage;
        Asteroid.OutOfBounds += MissMessage;
	}

    void onBeginTrial()
    {
        this.gameObject.GetComponent<Text>().text = "";
    }

    void onPracticeTrial()
    {
        this.gameObject.GetComponent<Text>();
    }

    void onEndTrials()
    {
        this.gameObject.GetComponent<Text>().text = "No More Trials";
        StartCoroutine("EndWindow");
    }

    void HitMessage()
    {
        this.gameObject.GetComponent<Text>().text = "Hit!";
        StartCoroutine("NextTrial");
    }
    void MissMessage()
    {
        this.gameObject.GetComponent<Text>().text = "Miss!";
        StartCoroutine("MissNextPassMessage");
    }

    public float time = 3f;

    IEnumerator EndMessage()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Text>().text = "End of Trials";
    }

    IEnumerator MissNextPassMessage()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<Text>().text = "";
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

    private void OnDisable()
    {
        TrialController.StartTrial -= onBeginTrial;
        TrialController.TrialsEnd -= onEndTrials;
        Asteroid.Hit -= HitMessage;
        Asteroid.OutOfBounds -= MissMessage;
    }
}

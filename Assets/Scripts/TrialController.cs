using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialController : MonoBehaviour {


    bool isPaused = false;

    public GameObject trial;

    public string stringToEdit = "Hello World";

    public bool trialStart = false;

    


    // Use this for initialization
    void Start () {
        

        
        
    }
	
	// Update is called once per frame
	void Update () {

        /// This is where the controller would read the XML data file
        /// Use a for loop to read the XML file and fill a queue of trial objects
        /// For the demo I will just be creating a trial case and hard coding the parameters 

        GameObject tempTrial = trial;

        // Load parameters into trial
        tempTrial.GetComponent<Trial>().shipSpawn = new Vector3(0f, -2.5f, 0f);
        tempTrial.GetComponent<Trial>().AsteroidMovementX = 1f;

        if (Input.GetKeyDown(KeyCode.Space) && trialStart != true)
        {
            trialStart = true;
            var createTrial = Instantiate(tempTrial, transform.position, transform.rotation);

            createTrial.transform.parent = gameObject.transform;
        }

        if(trialStart)
        {
            
        }
    }

    void PauseGame() {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = true;
        } else
        {
            Time.timeScale = 0;
            isPaused = false;
        }
    }

    void OnGUI()
    {
        
    }
}


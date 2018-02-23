using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialController : MonoBehaviour {

    bool isPaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
            Debug.Log("Game is Paused!");
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
}


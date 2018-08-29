using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridKeyListener : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G))
        {
            bool isGridActive = gameObject.GetComponent<GridOverlay>().enabled;
            gameObject.GetComponent<GridOverlay>().enabled = !isGridActive;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour {

    public bool shipMove = false;
    public bool shipRotate = false;

    public Vector3 shipSpawn = new Vector3(0, 0, 0);

    // We can implement the circle spawn idea and 
    //      do the math in AsteroidController
    public float AsteroidMovementX = 0;
    public float AsteroidMovementY = 0;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

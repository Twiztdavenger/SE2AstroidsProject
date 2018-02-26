using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour {

    

    // Ship attributes
    public Vector3 shipSpawn = new Vector3(0, 0, 0);

    public bool shipMove = false;
    public bool shipRotate = false;

    public float shipMoveSpeed = 1.5f;
    public float shipRotateSpeed = 180f;

    

    // Asteroid Object
    public GameObject asteroidPrefab;

    // Asteroid attributes
    public float AsteroidMovementX = 0;
    public float AsteroidMovementY = 0;
    public float AsteroidRotation = 180f;

    public Vector3 AsteroidSpawn = new Vector3(0, 0, 0);
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //GameObject tempAstroid = AsteroidList.Dequeue();

        GameObject tempAstroid = asteroidPrefab;

        tempAstroid.GetComponent<Asteroid>().rotationSpeed = AsteroidRotation;
        tempAstroid.GetComponent<Asteroid>().movementSpeedX = AsteroidMovementX;
        tempAstroid.GetComponent<Asteroid>().movementSpeedY = AsteroidMovementY;
        tempAstroid.GetComponent<Asteroid>().rotation = true;

        Instantiate(tempAstroid, AsteroidSpawn, transform.rotation);
    }
}

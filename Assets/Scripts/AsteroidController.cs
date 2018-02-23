using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    Queue<GameObject> AsteroidList = new Queue<GameObject>();

    public GameObject asteroidPrefab;

    public Vector3 spawnPoint = new Vector3(-10, 99, 0);

    public float rotationSpeed = 360f;
    public float movementSpeedX = 1.5f;

    public float movementSpeedY = 1.5f;

    // Use this for initialization
    void Start () {
        
        // ToDo:   

        //AsteroidList.Enqueue(asteroidPrefab);

        
	}
	
	// Update is called once per frame
	void Update () {

        // For testing purposes
        

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //GameObject tempAstroid = AsteroidList.Dequeue();

            GameObject tempAstroid = asteroidPrefab;

            tempAstroid.GetComponent<Asteroid>().rotationSpeed = rotationSpeed;
            tempAstroid.GetComponent<Asteroid>().movementSpeedX = movementSpeedX;
            tempAstroid.GetComponent<Asteroid>().movementSpeedY = movementSpeedY;
            tempAstroid.GetComponent<Asteroid>().rotation = true;

            Instantiate(tempAstroid, spawnPoint, transform.rotation);
        }
	}
}

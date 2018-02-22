using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    Queue<GameObject> AsteroidList;

    public GameObject asteroidPrefab;

    public Vector3 spawnPoint = new Vector3(-10, 99, 0);

    public float rotationSpeed = 360f;
    public float movementSpeed = 1.5f;

    // Use this for initialization
    void Start () {
        
        // BUG: For some reason this object is passing as a null object returning a null error
        //      Might be fixed when we have the XML document up and running

        //AsteroidList.Enqueue(asteroidPrefab);

        
	}
	
	// Update is called once per frame
	void Update () {

        // For testing purposes
        

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //GameObject tempAstroid = AsteroidList.Dequeue();

            GameObject tempAstroid = asteroidPrefab;

            tempAstroid.GetComponent<Astroid>().rotationSpeed = rotationSpeed;
            tempAstroid.GetComponent<Astroid>().movementSpeed = movementSpeed;
            tempAstroid.GetComponent<Astroid>().rotation = true;

            Instantiate(tempAstroid, spawnPoint, transform.rotation);
        }
	}
}

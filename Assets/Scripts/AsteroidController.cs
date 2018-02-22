using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    Queue<GameObject> AsteroidList;

    public GameObject asteroidPrefab;

    public Vector3 spawnPoint = new Vector3(-10, 99, 0);

	// Use this for initialization
	void Start () {
        

        //AsteroidList.Enqueue(asteroidPrefab);

        
	}
	
	// Update is called once per frame
	void Update () {

        // For testing purposes
        float rotationSpeed = 180f;
        float movementSpeed = 1.5f;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //GameObject tempAstroid = AsteroidList.Dequeue();

            GameObject tempAstroid = asteroidPrefab;

            tempAstroid.GetComponent<Astroid>().rotationSpeed = rotationSpeed;
            tempAstroid.GetComponent<Astroid>().movementSpeed = movementSpeed;
            tempAstroid.GetComponent<Astroid>().rotation = true;

            Instantiate(tempAstroid, new Vector3(-3, 1, 0), transform.rotation);
        }
	}
}

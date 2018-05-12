﻿using Assets.Scripts.Output;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassManager : MonoBehaviour{

    private static PassManager _instance;
    public static PassManager instance;

    public delegate void PassStages();
    public static event PassStages Shoot;
    public static event PassStages EndPass;
    public static event PassStages ProjHit;

    public GameObject shipPrefab;
    public GameObject asteroidPrefab;

    private bool wasFired = false;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        TrialController.BeginTrial += onBeginTrial;
        Asteroid.Hit += onEndTrial;

        Asteroid.OutOfBounds += onNextPass;
	}

    void onBeginTrial(TrialDataModel trialModel)
    {
        wasFired = false;

        // Load Asteroid 
        asteroidPrefab.GetComponent<Asteroid>().rotation = true;
        asteroidPrefab.GetComponent<Asteroid>().rotationSpeed = trialModel.AsteroidRotation;
        asteroidPrefab.GetComponent<Asteroid>().movementSpeedX = trialModel.AsteroidMovementX;
        asteroidPrefab.GetComponent<Asteroid>().movementSpeedY = trialModel.AsteroidMovementY;
        asteroidPrefab.GetComponent<Asteroid>().spawnPoint = new Vector3(trialModel.AsteroidSpawnX, trialModel.AsteroidSpawnY, 0);


        // Load Ship
        shipPrefab.GetComponent<PlayerMovement>().canMove = trialModel.ShipMove;
        shipPrefab.GetComponent<PlayerMovement>().canRotate = trialModel.ShipRotate;
        shipPrefab.GetComponent<PlayerMovement>().maxSpeed = trialModel.ShipMoveSpeed;
        shipPrefab.GetComponent<PlayerMovement>().rotSpeed = trialModel.ShipRotateSpeed;


        //Instantiate GameObjects
        Vector3 asteroidSpawnVector = new Vector3(trialModel.AsteroidSpawnX, trialModel.AsteroidSpawnY, 0);

        Debug.Log(asteroidSpawnVector);

        var createAsteroid = Instantiate(asteroidPrefab, asteroidSpawnVector, transform.rotation);
        createAsteroid.transform.parent = gameObject.transform;

        Vector3 shipSpawnVector = new Vector3(trialModel.ShipSpawnX, trialModel.ShipSpawnY, 0);

        Debug.Log(shipSpawnVector);

        var createShip = Instantiate(shipPrefab, shipSpawnVector, transform.rotation);
        createShip.transform.parent = gameObject.transform;

        
    }

    void onEndTrial()
    {
        Destroy(GameObject.FindGameObjectWithTag("Asteroid"));
        Destroy(GameObject.FindGameObjectWithTag("Ship"));


    }
	
	// Update is called once per frame
	void Update () {
        // SHOOTING
        // If we are pressing a fire button and bool did Fire was false
        if (Input.GetButton("Fire1") && !wasFired)
        {
            Shoot();
            wasFired = true;
        }
    }

    void onNextPass()
    {
        Debug.Log("Next Pass Started");
        wasFired = false;
    }

    private void OnDisable()
    {
        TrialController.BeginTrial -= onBeginTrial;
        Asteroid.OutOfBounds -= onNextPass;
        Asteroid.Hit -= onEndTrial;
    }
}

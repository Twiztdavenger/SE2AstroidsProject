using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour
{
    public bool asteroidDone = false;

    public GameObject shipPrefab;



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
    void Start()
    {
        //GameObject tempAstroid = AsteroidList.Dequeue();

        // Asteroid Parameters

        //asteroidPrefab = (GameObject)Instantiate(Resources.Load("Asteroid Large"));

        asteroidPrefab.GetComponent<Asteroid>().rotationSpeed = AsteroidRotation;
        asteroidPrefab.GetComponent<Asteroid>().movementSpeedX = AsteroidMovementX;
        asteroidPrefab.GetComponent<Asteroid>().movementSpeedY = AsteroidMovementY;
        asteroidPrefab.GetComponent<Asteroid>().rotation = true;


        
        // Ship Parameters

        //shipPrefab = (GameObject)Instantiate(Resources.Load("ship"));

        shipPrefab.GetComponent<PlayerMovement>().canMove = shipMove;
        shipPrefab.GetComponent<PlayerMovement>().canRotate = shipRotate;

        shipPrefab.GetComponent<PlayerMovement>().maxSpeed = shipMoveSpeed;
        shipPrefab.GetComponent<PlayerMovement>().rotSpeed = shipRotateSpeed;

        // Spawn Asteroid & Ship

        var createAsteroid = Instantiate(asteroidPrefab, AsteroidSpawn, transform.rotation);
        createAsteroid.transform.parent = gameObject.transform;

        var createShip = Instantiate(shipPrefab, shipSpawn, transform.rotation);
        createShip.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(asteroidDone == true)
        {
            gameObject.transform.parent.GetComponent<TrialController>().trialStart = false;

            Destroy(gameObject);
        }

    }

}

using Assets.Scripts.Output;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassManager : MonoBehaviour{

    private static PassManager _instance;
    public static PassManager instance;

    public delegate void PassStages();
    public static event PassStages BeginPass;
    public static event PassStages Shoot;
    public static event PassStages ProjHit;
    public static event PassStages EndOfTrial;

    public GameObject shipPrefab;
    public GameObject asteroidPrefab;

    private Vector3 AsteroidSpawnVector;

    public GameObject PassOutputDataCollector;

    private bool wasFired = false;
    private bool trialRunning = false;

    private float passTimer = 0f;
    private float timePlayerShotThisPass = 0f;
    
    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        TrialController.BeginTrial += BeginTrial;
        Asteroid.EndOfPass += EndPass;

        Instructions.CountdownOver += StartPass;
	}

    void BeginTrial(TrialDataModel trialModel)
    {
        passTimer = 0f;
        trialRunning = true;
        wasFired = false;

        // Load Asteroid 
        asteroidPrefab.GetComponent<Asteroid>().rotation = true;
        asteroidPrefab.GetComponent<Asteroid>().rotationSpeed = trialModel.AsteroidRotation;
        asteroidPrefab.GetComponent<Asteroid>().movementSpeedX = trialModel.AsteroidMovementX;
        asteroidPrefab.GetComponent<Asteroid>().movementSpeedY = trialModel.AsteroidMovementY;
        asteroidPrefab.GetComponent<Asteroid>().spawnPoint = new Vector3(trialModel.AsteroidSpawnX, trialModel.AsteroidSpawnY, 0);


        // Load Ship
        shipPrefab.GetComponent<Ship>().canMove = trialModel.ShipMove;
        shipPrefab.GetComponent<Ship>().canRotate = trialModel.ShipRotate;
        shipPrefab.GetComponent<Ship>().maxSpeed = trialModel.ShipMoveSpeed;
        shipPrefab.GetComponent<Ship>().rotSpeed = trialModel.ShipRotateSpeed;


        //Instantiate GameObjects
        AsteroidSpawnVector = new Vector3(trialModel.AsteroidSpawnX, trialModel.AsteroidSpawnY, 0);

        Vector3 shipSpawnVector = new Vector3(trialModel.ShipSpawnX, trialModel.ShipSpawnY, 0);

        var createShip = Instantiate(shipPrefab, shipSpawnVector, transform.rotation);
        createShip.transform.parent = gameObject.transform;

        BeginPass(); //Instructions.cs starts countdown, when countdown is over, run StartPass()
    }

    //TODO: Move Asteroid respawn logic here as well
    void StartPass()
    {
        wasFired = false;
        SpawnAsteroid();
        var spawnOutputDataCollector = Instantiate(PassOutputDataCollector);
    }

    void SpawnAsteroid()
    {
         var createAsteroid = Instantiate(asteroidPrefab, AsteroidSpawnVector, transform.rotation);
         createAsteroid.transform.parent = gameObject.transform;
    }

    void EndPass(bool wasAsteroidHit)
    {
        if(wasAsteroidHit)
        {
            trialRunning = false;
            EndOfTrial();
            StartCoroutine("DestroyDelay");
        } else
        {
            Debug.Log("Pass has ended");
            //Keeps us from firing during the countdown 
            wasFired = true;
            Destroy(GameObject.FindGameObjectWithTag("PassOutputDataCollector"));
        }
        
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(GameObject.FindGameObjectWithTag("Ship"));
        Destroy(GameObject.FindGameObjectWithTag("PassOutputDataCollector"));
        Destroy(GameObject.FindGameObjectWithTag("TrialOutputDataCollector"));
    }

    void Update () {
        passTimer += Time.deltaTime;

        // SHOOTING
        // If we are pressing a fire button and bool did Fire was false
        if (Input.GetButton("Fire1") && !wasFired && trialRunning)
        {
            Shoot();
            wasFired = true;
            timePlayerShotThisPass = passTimer;
        }
        
    } 

    private void OnDisable()
    {

    }
}

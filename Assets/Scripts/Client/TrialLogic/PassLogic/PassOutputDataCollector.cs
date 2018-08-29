using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassOutputDataCollector : MonoBehaviour {

    public GameObject PassManagerPrefab;

    //TODO: Add events to handle when PassManager calls BeginPass, Shoot, ProjHit, and EndPass
    //TODO: Add timers for when player shoots, how long each pass is
    //TODO: Find a way to collect minimum distance between asteroid and ship

    private float PassTimer = 0f;

    //Data for Collection
    public float TimePlayerShotInSeconds = 0f;
    public float ProjAsteroidMinDistance;
    public int NumberOfMisslesShot = 0;
    public bool ifAsteroidWasHit;
    public bool ifShipFired = false;

    public delegate void PassOutputReturnData(bool ifShipFired, bool ifAsteroidWasHit, float timePlayerShotInSeconds, float projAsteroidMinDistance);
    public static event PassOutputReturnData ReturnPassOutputData;

	// Use this for initialization
	void Start () {
        PassManager.Shoot += GetTimePlayerFired;
        Instructions.CountdownOver += ResetPassTimer;
        Projectile.FindProjAsteroidMinDistance += FindProjAsteroidMinDistance;
        Asteroid.EndOfPass += WasAsteroidHit;
	}
	
	// Update is called once per frame
	void Update () {
        PassTimer += Time.deltaTime;
	}

    private void GetTimePlayerFired()
    {
        TimePlayerShotInSeconds = PassTimer;
        Debug.Log(TimePlayerShotInSeconds);
        NumberOfMisslesShot++;
        ifShipFired = true;
    }

    private void WasAsteroidHit(bool hit)
    {
        ifAsteroidWasHit = hit;
    }

    private void ResetPassTimer()
    {
        PassTimer = 0f;
    }

    public void FindProjAsteroidMinDistance(float minDistance)
    {
        ProjAsteroidMinDistance = minDistance;
        Debug.Log(ProjAsteroidMinDistance);
    }

    private void OnDestroy()
    {
        if(ifAsteroidWasHit)
        {
            ProjAsteroidMinDistance = 0;
        }
        ReturnPassOutputData(ifShipFired, ifAsteroidWasHit, TimePlayerShotInSeconds, ProjAsteroidMinDistance);
        PassManager.Shoot -= GetTimePlayerFired;
        Instructions.CountdownOver -= ResetPassTimer;
        Projectile.FindProjAsteroidMinDistance -= FindProjAsteroidMinDistance;
        Asteroid.EndOfPass -= WasAsteroidHit;
    }
}

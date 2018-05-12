using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float maxSpeed = 5f;
    float timer = 1.5f;


    public float distance = 10000f;
    float tempDistance;

    public GameObject asteroid;
    public GameObject thisProjectile;

    public GameObject trialController;

    public GameObject ExplosionGO;

    private Vector2 projCloseCoordinates = new Vector2();
    private Vector2 astCloseCoordinates = new Vector2();


    void Start () {
        

        // This destroys the projectile after 'timer' amount of seconds
        Destroy(gameObject, timer);

        DistanceInfo.projMinX = 0f;
        DistanceInfo.projMinY = 0f;

        DistanceInfo.astMinX = 0f;
        DistanceInfo.astMinY = 0f;

    }

    void Update()
    {
        // This basically moves our projectile forward at speed maxSpeed
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        tempDistance = Vector2.Distance(asteroid.transform.position, transform.position);

        //if(tempDistance < distance)
        //{
        //    distance = tempDistance;

        //    projCloseCoordinates = transform.position;
        //    astCloseCoordinates = GameObject.FindGameObjectWithTag("Asteroid").transform.position;
        //}
       
    }

    void OnTriggerEnter2D()
    {
        /*
        GameObject.FindGameObjectWithTag("TrialController").GetComponent<TrialController>().LogCoord(projCloseCoordinates, astCloseCoordinates);

        DistanceInfo.projMinX = projCloseCoordinates.x;
        DistanceInfo.projMinY = projCloseCoordinates.y;

        DistanceInfo.astMinX = astCloseCoordinates.x;
        DistanceInfo.astMinY = astCloseCoordinates.y;
        */
        // When we enter a collision (astroid), destroy this projectile
        
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame

}

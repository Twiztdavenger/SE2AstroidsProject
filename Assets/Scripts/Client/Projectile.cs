using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float maxSpeed = 5f;
    float timer = 1.5f;


    public float distance = 10000f;
    float tempDistance;

    public float minDistanceFromAsteroid = 9999f;
    private float currentDistanceFromAsteroid;

    private bool foundMinDistance = false;

    public GameObject asteroid;
    public GameObject thisProjectile;

    public GameObject trialController;

    public GameObject ExplosionGO;

    private Vector2 projCloseCoordinates = new Vector2();
    private Vector2 astCloseCoordinates = new Vector2();

    public delegate void ProjData(float MinDistance);
    public static event ProjData FindProjAsteroidMinDistance;


    void Start () {
        // This destroys the projectile after 'timer' amount of seconds
        Destroy(gameObject, timer);

        try
        {
            asteroid = GameObject.FindGameObjectWithTag("Asteroid");
        } catch(Exception e)
        {
            Debug.Log("No Asteroid found");
        }
        
    }

    void Update()
    {
        // This basically moves our projectile forward at speed maxSpeed
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        if(asteroid != null && !foundMinDistance)
        {
            GameObject localAsteroid = GameObject.FindGameObjectWithTag("Asteroid");
            GameObject localProjectile = gameObject;

            Collider2D asteroidCol = localAsteroid.GetComponent<BoxCollider2D>();

            Collider2D projectileCol = localProjectile.GetComponent<BoxCollider2D>();

            Vector2 asteroidClosestPoint = asteroidCol.Distance(projectileCol).pointA;
            Vector2 projectileClosestPoint = asteroidCol.Distance(projectileCol).pointB;

            currentDistanceFromAsteroid = Vector2.Distance(asteroidClosestPoint, projectileClosestPoint);

            currentDistanceFromAsteroid = Vector3.Distance(gameObject.transform.position, asteroid.transform.position);

            if(currentDistanceFromAsteroid < minDistanceFromAsteroid)
            {
                minDistanceFromAsteroid = currentDistanceFromAsteroid;
            }
        }
    }

    void OnTriggerEnter2D()
    {
        // When we enter a collision (astroid), destroy this projectile
        
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        FindProjAsteroidMinDistance(minDistanceFromAsteroid);
    }
}

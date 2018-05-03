using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float maxSpeed = 5f;
    float timer = 1.5f;

    public GameObject asteroid;


    public float distance = 10000f;
    float tempDistance;

    void Start () {
        // This destroys the projectile after 'timer' amount of seconds
        Destroy(gameObject, timer);
	}

    void Update()
    {

        // This basically moves our projectile forward at speed maxSpeed
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        tempDistance = Vector3.Distance(transform.position, asteroid.transform.position);

        if(tempDistance < distance)
        {
            distance = tempDistance;
            Debug.Log(distance);
        }
        /*

        BoxCollider boxColProj = gameObject.GetComponent<BoxCollider>();
        Vector3 centerBoxProj = boxColProj.ClosestPointOnBounds(asteroid.transform.position);

        BoxCollider boxColAsteroid = asteroid.GetComponent<BoxCollider>();
        Vector3 centerBoxAst = boxColAsteroid.ClosestPointOnBounds(gameObject.transform.position);

        lineRenderer.SetPosition(0, centerBoxStart);
        lineRenderer.SetPosition(1, centerBoxEnd);*/

    }

    void OnTriggerEnter2D()
    {
        // When we enter a collision (astroid), destroy this projectile
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log(distance);
    }

    // Update is called once per frame

}

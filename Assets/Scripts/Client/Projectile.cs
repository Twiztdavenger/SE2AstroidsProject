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

    Collider2D projColl;
    Collider2D astColl;

    void Start () {
        projColl = thisProjectile.GetComponent<BoxCollider2D>();
        astColl = asteroid.GetComponent<BoxCollider2D>();

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
        tempDistance = projColl.Distance(astColl).distance;

        Debug.Log(projColl.Distance(astColl).isValid);

        if(tempDistance < distance)
        {
            distance = tempDistance;
            Debug.Log(distance);
        }
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float maxSpeed = 5f;
    float timer = 1.5f;
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

        
    }

    void OnTriggerEnter2D()
    {
        // When we enter a collision (astroid), destroy this projectile
        Destroy(gameObject);
    }

	// Update is called once per frame
	
}

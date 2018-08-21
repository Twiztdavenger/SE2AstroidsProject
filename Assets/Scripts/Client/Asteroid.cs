using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Asteroid : MonoBehaviour {

    /// 
    /// This object holds all of the logic for data output between passes
    /// 
    /// Update():
    ///     This update function is separated into 2 parts
    ///     
    ///     MOVEMENT
    ///         Vector3 pos grabs the transform.position
    ///         pos is then modified to move in a direction based on MovementX and MovementY
    ///         
    ///     PASS
    ///         screenRatio grabs the ratio between the width and height of the display
    ///         Camera.OrthographicSize is multiplied by the screen ratio to grab width.
    ///             This variable is widthOrtho
    ///         
    ///         These variables are then used to check if the asteroid in the X direction 
    ///             is greater than widthOrtho, the X boundaries of the camera view
    ///             
    ///             If pos.x IS greater than widthOrtho:
    ///                 Reset the asteroid to a position outside of the view
    ///                     This gives the illusion of "looping"
    ///             
    ///          Once we've verified if the asteroid is out of bounds
    ///             We know a 'pass' happened
    ///             
    ///             This portion grabs the parent object of our gameObject <TrialController>,
    ///                 and accesses a method called trialPass()
    ///                 
    ///                 trialPass() is a method that lets the trialController know a pass has happened
    ///                     We then pass a bunch of values (data may vary) to fill the PassDataModel with
    ///         
    ///         Since the asteroid left the view, we can assume it was still alive.
    ///             So therefore, hit = false
    ///             
    /// OnTriggerEnter2D():
    ///     This method is a collision detector that is activated
    ///         when we get hit by the projectile
    ///     
    ///     hit = true
    ///     
    ///     calls finishAsteroid()
    ///     
    /// finishAsteroid():
    ///     Lets our parent object the asteroid is destroyed
    ///     By hitting the asteroid, our pass has ended and we can move our data on
    ///         
    /// 

    public bool rotation = false;
    public float rotationSpeed = -180f;
    public float movementSpeedX= 1.5f;
    public float movementSpeedY = 1.5f;

    public bool hit = false;

    public float passTimer = 0f;

    public Vector3 spawnPoint;

    public delegate void AsteroidPass();
    public static event AsteroidPass OutOfBounds;
    public static event AsteroidPass Hit;

    public GameObject AsteroidAnim;

    void Update () {

        passTimer += Time.deltaTime;

        // MOVEMENT
        Vector3 pos = transform.position;
        pos += new Vector3 (movementSpeedX * Time.deltaTime, movementSpeedY * Time.deltaTime, 0);
        Quaternion rot = transform.rotation;
        float z = rot.eulerAngles.z;

        z -= rotationSpeed * Time.deltaTime;
        if (rotation) {
            rot = Quaternion.Euler (0, 0, z);
            transform.rotation = rot;
        }

        // PASS

        // Grabs screen ratio so we have a correct x boundary
        float screenRatio = (float) Screen.width / (float) Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        if (pos.x - 1f > widthOrtho || pos.y - 1f > Camera.main.orthographicSize) {
            pos = spawnPoint;
            // START NEW PASS FOR DATA COLLECTION
            OutOfBounds();


            //gameObject.transform.parent.GetComponent<TrialController>()
            //    .trialPass(numPasses, hit, passTimer);

            // If asteroid reaches end of screen, it was not hit
            hit = false;

            // Resets the time for the next pass
            passTimer = 0;
        }
        transform.position = pos;
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision Enter");

        // When we enter a collision with missile, destroy this asteroid
        if (col.gameObject.tag == "Projectile") {
            GameObject explosion = (GameObject)Instantiate(AsteroidAnim);
            explosion.transform.localScale += new Vector3(0.2F, 0.2F, 0);
            explosion.transform.position = transform.position;

            Debug.Log(-col.GetComponent<Rigidbody2D>().velocity.normalized);
            explosion.GetComponent<Rigidbody2D>().AddForce(-col.GetComponent<Rigidbody2D>().velocity.normalized * 5000, ForceMode2D.Impulse);


            hit = true;
            Hit();

            Destroy(gameObject);
        }
    }

    void finishAsteroid()
    {
        
        Destroy(gameObject);
        //gameObject.transform.parent.GetComponent<TrialController>().trialPass(numPasses, hit, Time.deltaTime * 60);
    }


}
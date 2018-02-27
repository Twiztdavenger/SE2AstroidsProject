using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public bool rotation = false;
    public float rotationSpeed = -180f;
    public float movementSpeedX= 1.5f;
    public float movementSpeedY = 1.5f;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;
        pos += new Vector3 (movementSpeedX * Time.deltaTime, movementSpeedY * Time.deltaTime, 0);
        Quaternion rot = transform.rotation;
        float z = rot.eulerAngles.z;
        // Change z angle based on input
        z -= rotationSpeed * Time.deltaTime;
        if (rotation) {
            // Recreate quaternion
            rot = Quaternion.Euler (0, 0, z);
            // Feed quaternion into our rotation
            transform.rotation = rot;
        }

        // Grabs screen ratio so we have a correct x boundary
        float screenRatio = (float) Screen.width / (float) Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        if (pos.x - 1f > widthOrtho) {
            pos.x = -widthOrtho - 1f;
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D (Collider2D col) {
        // When we enter a collision with missile, destroy this asteroid
        Debug.Log(col.ToString());
        if (col.gameObject.name == "missile(Clone)") {
            Destroy (gameObject);

            // Sets bool of our parent trial's variable to true
            gameObject.transform.parent.GetComponent<Trial>().asteroidDone = true;
        }
    }
}
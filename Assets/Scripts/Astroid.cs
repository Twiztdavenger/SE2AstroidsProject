﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour {

    public bool rotation = false;
    public float rotationSpeed = -180f;
    public float movementSpeed = 1.5f;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;

        pos += new Vector3 (movementSpeed * Time.deltaTime, 0, 0);

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

    void OnTriggerEnter2D () {
        // When we enter a collision (astroid), destroy this projectile
        Destroy (gameObject);
        /*
        Vector3 pos = transform.position;
        float screenRatio = (float) Screen.width / (float) Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;
        pos.x = -widthOrtho - 1f;
        transform.position = pos;*/

    }
}
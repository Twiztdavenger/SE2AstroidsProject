﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Variables for speed
    public float maxSpeed = 3.5f;
    public float rotSpeed = 180f;
    public float projSpeed = 5f;

    // Float for determining if ship is out of bounds or not
    float shipBoundaryRadius = 0.152f;

    // Booleans to determine if the ship can rotate/move or not
    public bool canRotate = false;
    public bool canMove = false;

    // Floats to control fire delay
    private float fireDelay = 0.25f;
    float coolDownTimer = 0;

    // Projectile we will be firing
    public GameObject missilePrefab;
    public bool wasFired;
    public float timeFired = 0f;

	void Update () {

        timeFired += Time.deltaTime;

        // ROTATATION
        Quaternion rot = transform.rotation;

        if (canRotate)
        {
            // Grab the Z euler angle
            float z = rot.eulerAngles.z;

            // Change z angle based on input
            z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

            // Recreate quaternion
            rot = Quaternion.Euler(0, 0, z);

            // Feed quaternion into our rotation
            transform.rotation = rot;
        }

        // MOVEMENT
        Vector3 pos = transform.position;

        if (canMove)
        {
            //MOVE the ship

            Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 0);
            pos += rot * velocity;


            // RESTRICT the player to the camera's boundaries

            // VERTICAL BOUNDARIES

            // If position of ship + our boundary radius is 
            // greater than the camera view in the y direction
            if (pos.y+shipBoundaryRadius > Camera.main.orthographicSize) 
            {
                // Reset the position of the ship so we don't exit the screen
                pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
            }
            if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
            {
                pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
            }

            // HORIZONTAL BOUNDARIES

            // Grabs screen ratio so we have a correct x boundary
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float widthOrtho = Camera.main.orthographicSize * screenRatio;

            if (pos.x + shipBoundaryRadius > widthOrtho)
            {
                pos.x = widthOrtho - shipBoundaryRadius;
            }
            if (pos.x - shipBoundaryRadius < -widthOrtho)
            {
                pos.x = -widthOrtho + shipBoundaryRadius;
            }

            // Change our position based upon the changes we made to pos
            transform.position = pos;
        }
        
        // SHOOTING

        // If we are pressing a fire button and bool didFire was false
        if(Input.GetButton("Fire1") && !wasFired)
        {
            missilePrefab.GetComponent<Projectile>().maxSpeed = projSpeed;

            // Set a spawn point to our objects rotation * .35 pixels above our origin point
            Vector3 offset = transform.rotation * new Vector3(0, .35f, 0);
            var projectile = Instantiate(missilePrefab, transform.position + offset, transform.rotation);

            // DATA COLLECTION
            wasFired = true;

        }

    }

}
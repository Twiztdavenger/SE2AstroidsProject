using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCameraCoordinates : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera camera = GetComponent<Camera>();
        Vector3 tr = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Vector3 tl = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
        Vector3 br = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
        Vector3 bl = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Debug.Log("Top Right: " + tr);
        Debug.Log("Top Right: " + tl);
        Debug.Log("Top Right: " + br);
        Debug.Log("Top Right: " + bl);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

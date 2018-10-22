using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResetAspectRatio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Camera>().aspect = 3f / 2f;
    }

}

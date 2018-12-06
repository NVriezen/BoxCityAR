using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardLook : MonoBehaviour {

    private Camera cam;

	// Use this for initialization
	void OnEnable () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cam.transform);
	}
}

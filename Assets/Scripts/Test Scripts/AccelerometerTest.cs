using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerTest : MonoBehaviour {

    Rigidbody body;

	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}

	void Update ()
    {
        Vector3 accel = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
        
        transform.position += accel;

	}
}

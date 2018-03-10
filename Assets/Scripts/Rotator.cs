using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotationSpeed = 0.1f;
    public Vector3 rotationAxis = Vector3.one / 2;

	void Update () {
        transform.RotateAround(transform.position, rotationAxis, rotationSpeed);
	}
}

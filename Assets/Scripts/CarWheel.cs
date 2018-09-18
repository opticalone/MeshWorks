using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour {

    public WheelCollider TargetWheelCollider;
    private Vector3 wheelPos = new Vector3();
    private Quaternion wheelRot = new Quaternion();
	
	// Update is called once per frame
	void Update () {
        TargetWheelCollider.GetWorldPose(out wheelPos, out wheelRot);
        transform.position = wheelPos;
        transform.rotation = wheelRot;
	}
}

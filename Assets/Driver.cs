using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour {


    public Transform path;
    public float maxSteerAngle = 45f;
    public float maxMotorTorque = 100f;
    public float switchDistance = 2f;
    public WheelCollider fr;
    public WheelCollider fl;
    public WheelCollider br;
    public WheelCollider bl;

    private List<Transform> nodes;
    private int currentNode = 0;

	// Use this for initialization
	void Start ()
    {
        Transform[] patrolTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < patrolTransforms.Length; i++)
        {
            if (patrolTransforms[i] != path.transform)
            { nodes.Add(patrolTransforms[i]); }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
        ApplySteering();
        Drive();
        CheckWaypoint();
	}
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, nodes[currentNode].position);
    }




    private void ApplySteering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        fl.steerAngle = newSteer;
        fr.steerAngle = newSteer;
        

    }

    private void Drive()
    {
        fl.motorTorque = -maxMotorTorque;
        fr.motorTorque = -maxMotorTorque;
        bl.motorTorque = -maxMotorTorque;
        br.motorTorque = -maxMotorTorque;
    }
    private void CheckWaypoint()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position)< switchDistance)
        {

        }
    }
}

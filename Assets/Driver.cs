
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour {

    [Header("Car & motor")]

    public float maxMotorBoost = 1.3f;
    public float motorBoost = 1.1f;
    private float boost = 1.0f;
    public float maxSteerAngle = 45f;
    public float minSteerAngle = 25f;
    public float maxMotorTorque = 100f;

    [Header("wheels")]
    public Transform path;
    public float switchDistance = 18f;
    public float maxSwitchDistance = 20f;
    public float minSwitchDistance = 10f;

    [ Header("wheels") ]
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
        boost = Random.Range(motorBoost, maxMotorBoost);
        switchDistance = Random.Range(maxSwitchDistance, minSwitchDistance);
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, nodes[currentNode].position);
    }




    private void ApplySteering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        fl.steerAngle = newSteer;
        fr.steerAngle = newSteer;
       // Debug.Log(newSteer);

    }

    private void Drive()
    {
        fl.motorTorque = (maxMotorTorque * boost); 
        fr.motorTorque = (maxMotorTorque * boost); 
        bl.motorTorque = (maxMotorTorque * boost);
        br.motorTorque = (maxMotorTorque * boost); 
    }
    private void CheckWaypoint()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position)< switchDistance)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}

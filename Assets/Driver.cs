
using System;
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
    public float maxSpeed = 400f;
    public float currentSpeed;
    [Header("brakes")]
    public Vector3 CenterOfMass;

    public bool isBreaking = false;
    public GameObject brakeLights;
    //public Material brakeLightMat;
    public float maxBrakTorque = 150f;

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

    [Header("sensors")]

    public float sensorLength = 10f;
    public float sensorOffset = 1f;
    public float sensorSideOffset = .5f;
    public float sensorSideAngle = 35f;

    private List<Transform> nodes;
    private int currentNode = 0;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().centerOfMass = CenterOfMass;
        Transform[] patrolTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < patrolTransforms.Length; i++)
        {
            if (patrolTransforms[i] != path.transform)
            { nodes.Add(patrolTransforms[i]); }
        }
        boost = UnityEngine.Random.Range(motorBoost, maxMotorBoost);
        switchDistance = UnityEngine.Random.Range(maxSwitchDistance, minSwitchDistance);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Sense();
        ApplySteering();
        ApplyBrakes();
        Drive();
        CheckWaypoint();
	}

    private void Sense()
    {
        RaycastHit hit;
        Vector3 SensorStartPos = transform.position;
        SensorStartPos.z += sensorOffset;
        
        if (Physics.Raycast(SensorStartPos,transform.forward, out hit,sensorLength))
        {

        }
        Debug.DrawRay(SensorStartPos, hit.point);
        SensorStartPos.x += sensorSideOffset;
        if (Physics.Raycast(SensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawRay(SensorStartPos, hit.point);
        if (Physics.Raycast(SensorStartPos, Quaternion.AngleAxis(sensorSideAngle, transform.up)*transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawRay(SensorStartPos, hit.point);
        SensorStartPos.x -= sensorSideOffset;
        if (Physics.Raycast(SensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawRay(SensorStartPos, hit.point);
        if (Physics.Raycast(SensorStartPos, Quaternion.AngleAxis(-sensorSideAngle, transform.up) * transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawRay(SensorStartPos, hit.point);
    }

    private void ApplyBrakes()
    {
        if (isBreaking)
        {
            br.brakeTorque = maxBrakTorque;
            bl.brakeTorque = maxBrakTorque;
            brakeLights.SetActive(true);
        }
        else
        {
            br.brakeTorque = 0;
            bl.brakeTorque = 0;
            brakeLights.SetActive(false);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, nodes[currentNode].position);
    //}




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
        currentSpeed = 2 * Mathf.PI * fl.radius * fl.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && !isBreaking)
        {
            fl.motorTorque = (boost);
            fr.motorTorque = (boost);
            bl.motorTorque = (maxMotorTorque * boost);
            br.motorTorque = (maxMotorTorque * boost);
        }
        else
        {
            fl.motorTorque = 0;
            fr.motorTorque = (0);
            bl.motorTorque = (0);
            br.motorTorque = (0);
        }
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

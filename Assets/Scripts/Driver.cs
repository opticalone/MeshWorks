﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour {

    [Header(" C a r   &   m o t o r ")]

    public float maxMotorBoost = 1.3f;
    public float motorBoost = 1.1f;
    private float boost = 1.0f; 
    public float maxMotorTorque = 100f;
    public float maxSpeed = 37f;
    public float currentSpeed;

    [Header(" b r a k e s ")]
    public Vector3 CenterOfMass;
    public bool isBreaking = false;
    public float brakeDistance = 30.0f;
    public GameObject brakeLights;
    
    public float maxBrakTorque = 150f;
    public float breakMultiplier; 

    [Header(" w h e e l s ")]
    public Transform path;
    public float switchDistance = 18f;
    public float maxSwitchDistance = 20f;
    public float minSwitchDistance = 10f;
    public float maxSteerAngle = 45f;
    public float minSteerAngle = 25f;
    private float decidedSteerAngle;
    private float targetSteerAngle = 0;
    public float turnSpeed = 5.0f;

    [ Header(" w h e e l s   c  o l l i d e r ") ]
    public WheelCollider fr;
    public WheelCollider fl;
    public WheelCollider br;
    public WheelCollider bl;

    [Header(" s e n s o r s ")]

    public Transform sensorHolder;
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
        decidedSteerAngle = UnityEngine.Random.Range(maxSteerAngle, minSteerAngle);
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Sense();
        ApplySteering();
        //ApplyBrakes();
        Drive();
        CheckWaypoint();
        //SlowOnApproach();
        

    }

    //private void Sense()
    //{
    //    RaycastHit hit;
    //    Vector3 SensorStartPos = sensorHolder.position;
    //    SensorStartPos.z += sensorOffset;
        
    //    if (Physics.Raycast(SensorStartPos,transform.forward, out hit, sensorLength))
    //    {

    //    }

    //   // Debug.DrawLine(SensorStartPos, hit.point);

    //    SensorStartPos.x += sensorSideOffset;


    //    if (Physics.Raycast(SensorStartPos, transform.forward, out hit, sensorLength))
    //    {

    //    }

    //  //  Debug.DrawLine(SensorStartPos, hit.point);
    //    if (Physics.Raycast(SensorStartPos, Quaternion.AngleAxis(sensorSideAngle, transform.up) * transform.forward, out hit, sensorLength))
    //    {

    //    }
    // //   Debug.DrawLine(SensorStartPos, hit.point);

    //    SensorStartPos.x -= sensorSideOffset;


    //    if (Physics.Raycast(SensorStartPos, transform.forward, out hit, sensorLength))
    //    {

    //    }

    //   // Debug.DrawLine(SensorStartPos, hit.point);
    //    if (Physics.Raycast(SensorStartPos, Quaternion.AngleAxis(-sensorSideAngle, transform.up) * transform.forward, out hit, sensorLength))
    //    {

    //    }
    //  //  Debug.DrawLine(SensorStartPos, hit.point);
    //}

    private void ApplyBrakes()
    {
        breakMultiplier = currentSpeed / brakeDistance;
        if (isBreaking)
        {
            br.brakeTorque = maxBrakTorque * breakMultiplier;
            bl.brakeTorque = maxBrakTorque * breakMultiplier;
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

        float newSteer = (relativeVector.x / relativeVector.magnitude) * decidedSteerAngle;
        targetSteerAngle = newSteer;
        //fl.steerAngle = newSteer;
        //fr.steerAngle = newSteer;
        fl.steerAngle = Mathf.Lerp(fl.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        fr.steerAngle = Mathf.Lerp(fr.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        // Debug.Log(newSteer);

    }

    private void Drive()
    {
        currentSpeed = (2 * Mathf.PI * fl.radius * fl.rpm * 60 / 1000);
        
        if (currentSpeed < maxSpeed && !isBreaking)
        {
            
            bl.motorTorque = (maxMotorTorque * boost);
            br.motorTorque = (maxMotorTorque * boost);
        }
        else
        {
           // isBreaking = true;
            fl.motorTorque = 0;
            fr.motorTorque = 0;
            bl.motorTorque = 0;
            br.motorTorque = 0;
          
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

    void LerpToSteer()
    {
        fl.steerAngle = Mathf.Lerp(fl.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        fr.steerAngle = Mathf.Lerp(fr.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }

    private void SlowOnApproach()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < brakeDistance)
        {
            StartCoroutine(doBrakes());

        }
        else { isBreaking = false; }
    }

    IEnumerator doBrakes()
    {
        isBreaking = true;
        yield return new WaitForSecondsRealtime(2);
        isBreaking = false;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestPlayerControllerEber : MonoBehaviour {

    public Rigidbody rb;
    public Animator anim;
    public float speed = 5f;

	// Use this for initialization
	void Start () {
        rb.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));        
        rb.velocity = input * speed;     
        anim.SetFloat("Speed", rb.velocity.magnitude);
	}

    //private Vector3 bsGrav()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {return Physics.gravity* -1;}
    //    else
    //    { return Physics.gravity; }

    //}
}

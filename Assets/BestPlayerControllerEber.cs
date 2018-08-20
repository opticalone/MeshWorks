﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestPlayerControllerEber : MonoBehaviour {

    public Rigidbody rb;
    public Animator anim;

	// Use this for initialization
	void Start () {
        rb.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), (Input.GetAxis("Jump")), (Input.GetAxis("Vertical")));
        rb.velocity = input * 2;
        anim.SetFloat("Speed", rb.velocity.magnitude);
	}
}

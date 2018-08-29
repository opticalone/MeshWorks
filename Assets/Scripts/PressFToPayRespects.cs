using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFToPayRespects : MonoBehaviour {


    public GameObject obj;
    public Transform point;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("f"))
        {
            PayRespects();
        }
	}

    void PayRespects()
    {
        Instantiate(obj, point.position, transform.rotation);
    }
}

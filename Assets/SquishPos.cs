using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishPos : MonoBehaviour {


    public Material squishMat;
    // Use this for initialization
    void Start () {
		
	}
    private void OnCollisionEnter(Collision coll)
    {
        
        squishMat.SetVector("_SquishVec", (coll.contacts[0].point));
    }
    private void OnCollisionStay(Collision coll)
    {
        squishMat.SetVector("_SquishVec", (coll.contacts[0].point));
    }
    // Update is called once per frame
    void Update () {
		
	}
}

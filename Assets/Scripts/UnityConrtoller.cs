using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityConrtoller : MonoBehaviour {

    public Animator anim;
	// Use this for initialization
	void Reset () {
        bool bose1 = Input.GetKeyDown(KeyCode.Q);
        bool bose2 = Input.GetKeyDown(KeyCode.W);
        bool bose3 = Input.GetKeyDown(KeyCode.E);

        if (bose1) { anim.SetTrigger("Anim1"); }
        if (bose2) { anim.SetTrigger("Anim2"); }
        if (bose3) { anim.SetTrigger("Anim3"); }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

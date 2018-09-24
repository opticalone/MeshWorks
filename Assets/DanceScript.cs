using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScript : MonoBehaviour {

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("changeDance", true);
        }
        else { anim.SetBool("changeDance", false); }
	}
}

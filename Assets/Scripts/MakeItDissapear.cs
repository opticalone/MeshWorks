using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItDissapear : MonoBehaviour {

    public MeshRenderer mat;
	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Jump")>0)
        {
     
            changeColors();

        }
	}
    void changeColors()
    {
        var col = new Color();
        col = mat.material.color;
        col.a -= 10;
        //col.a = Mathf.Clamp(col.a, 0.0f, 255.0f);
        mat.material.color = col;
    }

}

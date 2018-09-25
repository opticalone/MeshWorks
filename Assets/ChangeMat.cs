using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour {

    MeshRenderer rend;
    public Material mat1;
    public Material mat2;
    int count = 0;

    // Use this for initialization
    void Start () {
        rend = GetComponent<MeshRenderer>();
       
	}

     public void skinSwitch()
    {
        count++;
        if (count == 1)
        {
            rend.material = mat1;
        }
        if (count == 2)
        {
            rend.material = mat2;
        }
        if (count == 2)
        {
            count = 0;
        }
   
       
            
       
     

    }
}

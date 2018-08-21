using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDeform : MonoBehaviour {

    public float collideRadius = .5f;
    public DeformMesh deformMesh;
	    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            deformMesh.AddDepression(contact.point, collideRadius);
        }
        
    }
}

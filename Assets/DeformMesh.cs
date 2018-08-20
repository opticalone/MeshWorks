using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(MeshGen))]
public class DeformMesh : MonoBehaviour {

    public float maxDepression;
    public Vector3[] OGverts;
    public Vector3[] modVerts;

    private MeshGen plane;
    public void MeshRegenerated()
    {
        plane = GetComponent<MeshGen>();
        OGverts = plane.mesh.vertices.ToArray();
        modVerts = plane.mesh.vertices;

        modVerts[5] = modVerts[5] + Vector3.down;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(MeshGen))]
public class DeformMesh : MonoBehaviour {

    public float maxDepression;
    public List<Vector3> OGverts;
    public List<Vector3> modVerts;

    private MeshGen plane;

    public void Update()
    {       
       plane.mesh.SetVertices(modVerts);
    }

    public void MeshRegenerated()
    {
        plane = GetComponent<MeshGen>();
        plane.mesh.MarkDynamic();

        OGverts = plane.mesh.vertices.ToList();
        modVerts = plane.mesh.vertices.ToList();
        Debug.Log("oh, i generated a mesh. great.");
    
        
    }
    public void AddDepression(Vector3 depressionPoint, float radius)
    {
        var worldPos4 = this.transform.worldToLocalMatrix * depressionPoint;
        var worldPos = new Vector3(worldPos4.x, worldPos4.y, worldPos4.z);
        for (int i = 0; i < modVerts.Count; ++i)
        {
            var distance = (worldPos - (modVerts[i] + Vector3.down * maxDepression)).magnitude;
            if (distance < radius)
            {
                var newVert = OGverts[i] + Vector3.down * maxDepression;
                modVerts.RemoveAt(i);
                modVerts.Insert(i, newVert);
            }
           
        }
        
        Debug.Log("hecc now im depressed");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour
{

    private MeshFilter filter;
    public Mesh mesh
    {
        get { return filter.mesh; }
    }
    public float size = 1;
    public int gridSize = 16;
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
        SendMessage("MeshRegenerated");
    }

    Mesh GenerateMesh()
    {

        Mesh mesh = new Mesh();
        var verticies = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        for (int x = 0; x < gridSize+1; ++x)
        {
            for (int y = 0; y < gridSize+1; ++y)
            {
                //calculate x and y of our grid
                //start in top corner and itterate over x and y
                verticies.Add(new Vector3(-size * .5f + size * (x / ((float)gridSize)), 0, -size * .5f + size * (y / ((float)gridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)gridSize, y / (float)gridSize));
            }
        }

        var triangles =   new List<int>();
        var vertCount = gridSize + 1;

        for (int i = 0; i < vertCount*vertCount-vertCount; ++i)
        {

            if ((i+1) % vertCount == 0)
            {
                continue;
            }
            triangles.AddRange(new List<int>()
            {
                i+1 + vertCount, i + vertCount, i,
                i, i+1, i+vertCount+1
            });
            
        }
      
        mesh.SetVertices(verticies);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);
        

        
        return mesh;

    }
}

////SETS VERTS
//mesh.SetVertices(new List<Vector3>()
//{
//    new Vector3(-size * 0.5f, 0, -size *.5f),
//     new Vector3(size * 0.5f, 0, -size *.5f),
//      new Vector3(size * 0.5f, 0, size *.5f),
//       new Vector3(-size * 0.5f, 0, size *.5f)

//});


//SETS TRIS
////SETS NORMALS
//mesh.SetNormals(new List<Vector3>()
//{   -Vector3.up,
//    -Vector3.up,
//    -Vector3.up,
//    -Vector3.up
//});

////SETS UVS
//mesh.SetUVs(0, new List<Vector2>()
//{
//    new Vector2(0,0),
//    new Vector2(1,0),
//    new Vector2(1,1),
//    new Vector2(0,1)
//});


////////////using System.Collections;
////////////using System.Collections.Generic;
////////////using UnityEngine;

////////////public class CodeQuad : MonoBehaviour
////////////{
////////////    void Start()
////////////    {
////////////        var filter = GetComponent<MeshFilter>();
////////////        var mesh = new Mesh();
////////////        filter.mesh = mesh;

////////////        // Vertices
////////////        // locations of vertices
////////////        var verts = new Vector3[3];

////////////        verts[0] = new Vector3(0, 0, 0);
////////////        verts[1] = new Vector3(1, 0, 0);
////////////        verts[2] = new Vector3(0, 1, 0);
////////////        mesh.vertices = verts;

////////////        // Indices
////////////        // determines which vertices make up an individual triangle
////////////        var indices = new int[6];

////////////        indices[0] = 0;
////////////        indices[1] = 2;
////////////        indices[2] = 1;

////////////        mesh.triangles = indices;

////////////        // Normals
////////////        // describes how light bounces off the surface (at the vertex level)
////////////        var norms = new Vector3[3];

////////////        norms[0] = -Vector3.forward;
////////////        norms[1] = -Vector3.forward;
////////////        norms[2] = -Vector3.forward;

////////////        mesh.normals = norms;

////////////        // UVs, STs
////////////        // defines how textures are mapped onto the surface
////////////        var UVs = new Vector2[3];

////////////        UVs[0] = new Vector2(0, 0);
////////////        UVs[1] = new Vector2(1, 0);
////////////        UVs[2] = new Vector2(0, 1);

////////////        mesh.uv = UVs;
////////////    }
////////////}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ProceduralD12 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    List<Vector3> vertices;
    List<int> triangles;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeD12();
        UpdateMesh();
        meshCollider.convex = true;
    }

    void MakeD12()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 12; i++)     // i < number of faces shape has
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        vertices.AddRange(D12MeshData.faceVertices(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 5);      // 1 group of 0-2 means 3 total vertices per face
        triangles.Add(vCount - 5 + 1);
        triangles.Add(vCount - 5 + 2);
        triangles.Add(vCount - 5);      
        triangles.Add(vCount - 5 + 2);
        triangles.Add(vCount - 5 + 3);
        triangles.Add(vCount - 5);      
        triangles.Add(vCount - 5 + 3);
        triangles.Add(vCount - 5 + 4);

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}

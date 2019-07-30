using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class ProceduralD10 : MonoBehaviour
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
        MakeD10();
        UpdateMesh();
        meshCollider.convex = true;
    }

    void MakeD10()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        vertices.AddRange(D10MeshData.faceVertices(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);


    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}

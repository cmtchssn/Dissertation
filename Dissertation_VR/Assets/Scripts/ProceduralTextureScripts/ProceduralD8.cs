using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ProceduralD8 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshRend = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeD8();
        UpdateMesh();
        meshCollider.convex = true;
    }

    void MakeD8() //consider putting this in mesh data file.
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 8; i++) //8 sides
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        vertices.AddRange(D8MeshData.faceVertices(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 3);
        triangles.Add(vCount - 3 + 1);
        triangles.Add(vCount - 3 + 2);
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
    }
}

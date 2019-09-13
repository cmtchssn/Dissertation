using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ProceduralD6 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;

    static float C0 = 1f;

    public static Vector3[] verticesD6 =
    {
        new Vector3( C0,  C0,  C0),
        new Vector3( C0,  C0, -C0),
        new Vector3( C0, -C0,  C0),
        new Vector3( C0, -C0, -C0),
        new Vector3(-C0,  C0,  C0),
        new Vector3(-C0,  C0, -C0),
        new Vector3(-C0, -C0,  C0),
        new Vector3(-C0, -C0, -C0)
    };

    public static int[][] faceTrianglesD6 =
    {
        new int[] { 0, 1, 5, 4 },
        new int[] { 0, 4, 6, 2 },
        new int[] { 0, 2, 3, 1 },
        new int[] { 7, 3, 2, 6 },
        new int[] { 7, 6, 4, 5 },
        new int[] { 7, 5, 1, 3 }
    };

    public static Vector3[] faceVerticesD6(int dir)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD6[faceTrianglesD6[dir][i]];
        }
        return fv;
    }

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
        MakeD6();
        UpdateMesh();
        meshCollider.convex = true;
    }

    void MakeD6()
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
        vertices.AddRange(faceVerticesD6(dir));
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
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
    }
}

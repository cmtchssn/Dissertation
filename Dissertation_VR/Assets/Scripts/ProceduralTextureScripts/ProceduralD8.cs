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

    public static Vector3[] verticesD8 =
    {
        new Vector3( 0,  1,  0),
        new Vector3( 1,  0,  1),
        new Vector3(-1,  0,  1),
        new Vector3(-1,  0, -1),
        new Vector3( 1,  0, -1),
        new Vector3( 0, -1,  0)
    };

    public static int[][] faceTrianglesD8 =
    {
        //top triangles
        new int[] { 2, 1, 0 },
        new int[] { 3, 2, 0 },
        new int[] { 4, 3, 0 },
        new int[] { 1, 4, 0 },
        //bottom triangles
        new int[] { 1, 2, 5 },
        new int[] { 2, 3, 5 },
        new int[] { 3, 4, 5 },
        new int[] { 4, 1, 5 }   //culling is weird on this one, these are counterclockwise, but work.
    };

    public static Vector3[] faceVerticesD8(int dir)
    {
        Vector3[] fv = new Vector3[3];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD8[faceTrianglesD8[dir][i]];
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
        vertices.AddRange(faceVerticesD8(dir));
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

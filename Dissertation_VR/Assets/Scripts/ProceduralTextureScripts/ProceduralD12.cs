using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ProceduralD12 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;

    static float C0 = 0.809016994374947424102293417183f;    // = (1f + Math.Sqrt(5f)) / 4f
    static float C1 = 1.30901699437494742410229341718f;     // = (3f + Math.Sqrt(5f)) / 4f

    public static Vector3[] verticesD12 =      // where each face connects in space
    {
        new Vector3( 0.0f,  0.5f,    C1),
        new Vector3( 0.0f,  0.5f,   -C1),
        new Vector3( 0.0f, -0.5f,    C1),
        new Vector3( 0.0f, -0.5f,   -C1),
        new Vector3(   C1,  0.0f,  0.5f),
        new Vector3(   C1,  0.0f, -0.5f),
        new Vector3(  -C1,  0.0f,  0.5f),
        new Vector3(  -C1,  0.0f, -0.5f),
        new Vector3( 0.5f,    C1,  0.0f),
        new Vector3( 0.5f,   -C1,  0.0f),
        new Vector3(-0.5f,    C1,  0.0f),
        new Vector3(-0.5f,   -C1,  0.0f),
        new Vector3(   C0,    C0,    C0),
        new Vector3(   C0,    C0,   -C0),
        new Vector3(   C0,   -C0,    C0),
        new Vector3(   C0,   -C0,   -C0),
        new Vector3(  -C0,    C0,    C0),
        new Vector3(  -C0,    C0,   -C0),
        new Vector3(  -C0,   -C0,    C0),
        new Vector3(  -C0,   -C0,   -C0)
    };

    public static int[][] faceTrianglesD12 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] {  0,  2, 14,  4, 12 },
        new int[] {  0, 12,  8, 10, 16 },
        new int[] {  0, 16,  6, 18,  2 },
        new int[] {  7,  6, 16, 10, 17 },
        new int[] {  7, 17,  1,  3, 19 },
        new int[] {  7, 19, 11, 18,  6 },
        new int[] {  9, 11, 19,  3, 15 },
        new int[] {  9, 15,  5,  4, 14 },
        new int[] {  9, 14,  2, 18, 11 },
        new int[] { 13,  1, 17, 10,  8 },
        new int[] { 13,  8, 12,  4,  5 },
        new int[] { 13,  5, 15,  3,  1 }
    };

    public static Vector3[] faceVerticesD12(int dir)
    {
        Vector3[] fv = new Vector3[5]; // number of vertices per face
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD12[faceTrianglesD12[dir][i]];
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
        Debug.Log(vertices.Count);

    }

    void MakeFace(int dir)
    {
        vertices.AddRange(faceVerticesD12(dir));
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
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
    }
}

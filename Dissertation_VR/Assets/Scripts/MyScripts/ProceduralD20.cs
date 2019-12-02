using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD20 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;
    bool colFlag = false;
    bool reTrig = true;
    int faceVal;
    AudioClipScript bell;
    Vector3[][] face;
    static int faceCount = 20;
    int faceVertCount = 3;

    static float C0 = 0.809016994374947424102293417183f;    // = (1f + Mathf.Sqrt(5f)) / 4f;

    public static Vector3[] verticesD20 =      // where each face connects in space
    {
        new Vector3( 0.5f,  0.0f,    C0),
        new Vector3( 0.5f,  0.0f,   -C0),
        new Vector3(-0.5f,  0.0f,    C0),
        new Vector3(-0.5f,  0.0f,   -C0),
        new Vector3(   C0,  0.5f,  0.0f),
        new Vector3(   C0, -0.5f,  0.0f),
        new Vector3(  -C0,  0.5f,  0.0f),
        new Vector3(  -C0, -0.5f,  0.0f),
        new Vector3( 0.0f,    C0,  0.5f),
        new Vector3( 0.0f,    C0, -0.5f),
        new Vector3( 0.0f,   -C0,  0.5f),
        new Vector3( 0.0f,   -C0, -0.5f)
    };

    public static int[][] faceTrianglesD20 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] {  0,  2, 10 },
        new int[] {  0, 10,  5 },
        new int[] {  0,  5,  4 },
        new int[] {  0,  4,  8 },
        new int[] {  0,  8,  2 },
        new int[] {  3,  1, 11 },
        new int[] {  3, 11,  7 },
        new int[] {  3,  7,  6 },
        new int[] {  3,  6,  9 },
        new int[] {  3,  9,  1 },
        new int[] {  2,  6,  7 },
        new int[] {  2,  7, 10 },
        new int[] { 10,  7, 11 },
        new int[] { 10, 11,  5 },
        new int[] {  5, 11,  1 },
        new int[] {  5,  1,  4 },
        new int[] {  4,  1,  9 },
        new int[] {  4,  9,  8 },
        new int[] {  8,  9,  6 },
        new int[] {  8,  6,  2 }
    };

    public static Vector3[] faceVerticesD20(int dir)
    {
        Vector3[] fv = new Vector3[3]; // number of vertices per face
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD20[faceTrianglesD20[dir][i]];
        }
        return fv;
    }

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshRend = GetComponent<MeshRenderer>();
        face = new Vector3[faceCount][];
    }

    void Start()
    {
        MakeD20();
        UpdateMesh();
        meshCollider.convex = true;
        bell = GetComponent<AudioClipScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (reTrig && collision.collider.tag == "Floor")
        {
            reTrig = false;
            colFlag = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (colFlag)
        {
            string[] globalFace = new string[faceCount];
            for (int i = 0; i < faceCount; i++)
            {
                for (int j = 0; j < faceVertCount; j++)
                {
                    globalFace[i] = globalFace[i] + transform.TransformPoint(face[i][j]).ToString();
                }
            }

            if (collision.contactCount == faceVertCount)
            {
                string col1 = collision.GetContact(0).point.ToString();
                string col2 = collision.GetContact(1).point.ToString();
                string col3 = collision.GetContact(2).point.ToString();

                for (int i = 0; i < faceCount; i++)
                {
                    if (globalFace[i].Contains(col1) && globalFace[i].Contains(col2) && globalFace[i].Contains(col3))
                    {
                        //print("D20 Face " + (i + 1) + " colliding");
                        faceVal = i;
                        //pause then play audio.
                        bell.Toll(0, 20 - faceVal);
                        colFlag = false;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        print(collision.gameObject.tag);
        if (collision.collider.tag == "Floor")
        {
            bell.Stop();
            reTrig = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            bell.Toll(0, faceVal);
        }
        if (other.gameObject.layer == 16)
        {
            bell.Toll(0, 22 - faceVal);
        }
    }

    void MakeD20()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < faceCount; i++)     // i < number of faces shape has
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        face[dir] = faceVerticesD20(dir);
        vertices.AddRange(faceVerticesD20(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 3);      // 1 group of 0-2 means 3 total vertices per face
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

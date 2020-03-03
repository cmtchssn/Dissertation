using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD12 : MonoBehaviour
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
    static int faceCount = 12;
    int faceVertCount = 5;

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
        face = new Vector3[faceCount][];
    }

    void Start()
    {
        MakeD12();
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
                    //print(globalFace[i]);
                }
            }

            if (collision.contactCount == (faceVertCount - 1))
            {
                string col1 = collision.GetContact(0).point.ToString();
                string col2 = collision.GetContact(1).point.ToString();
                string col3 = collision.GetContact(2).point.ToString();
                string col4 = collision.GetContact(3).point.ToString();

                for (int i = 0; i < faceCount; i++)
                {
                    if (globalFace[i].Contains(col1) && globalFace[i].Contains(col2) && globalFace[i].Contains(col3) && globalFace[i].Contains(col4))
                    {
                        print("D12 Face " + (i + 1) + " colliding");
                        faceVal = i;
                        //pause then play audio.
                        //bell.Toll(0, faceVal);
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
            if (faceVal >= 11)
            {
                bell.Toll(8, faceVal - 2);
            }
            else
            {
                bell.Toll(14, faceVal);
            }
        }
        if (other.gameObject.layer == 16)
        {
            if (faceVal >= 11)
            {
                bell.Toll(9, faceVal - 2);
            }
            else
            {
                bell.Toll(9, faceVal);
            }
        }
    }

    void MakeD12()
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
        face[dir] = faceVerticesD12(dir);
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

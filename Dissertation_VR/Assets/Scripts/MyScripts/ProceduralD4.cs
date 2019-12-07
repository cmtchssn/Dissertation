using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD4 : MonoBehaviour
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
    int faceCount = 4;
    static int faceVertCount = 3;

    #region D4 Stats

    static float C0 = 0.353553390593273762200422181052f;// = Mathf.Sqrt(2f) / 4f;
    //public static float size = 2f;
    static float C1 = C0 * 2f;

    public static Vector3[] verticesD4 =      // where each face connects in space
    {
        new Vector3( C1, -C1,  C1),
        new Vector3( C1,  C1, -C1),
        new Vector3(-C1,  C1,  C1),
        new Vector3(-C1, -C1, -C1)
    };

    //static int[][] faceytri = D4;

    public static int[][] faceTrianglesD4 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] { 0, 1, 2 },
        new int[] { 1, 0, 3 },
        new int[] { 2, 3, 0 },
        new int[] { 3, 2, 1 }
    };
    /*
    static int[][] D4 =
    {
        { 0, 1, 2 },
        { 1, 0, 3 },
        { 2, 3, 0 },
        { 3, 2, 1 }
    }
    */
    public Vector3[] faceVerticesD4(int dir)
    {
        Vector3[] fv = new Vector3[faceVertCount]; // number of vertices per face

        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD4[faceTrianglesD4[dir][i]];
        }

        return fv;
    }
    #endregion

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshRend = GetComponent<MeshRenderer>();
        face = new Vector3[faceCount][];
        
        // Spawn resize info here:
        // I think using transform scale may be the easy way to do it
        //this.verticesD4
        //size = size + 0.5f;
        //C1 = C0 * size;
        //Debug.Log(size);
        //Debug.Log(C1);
    }

    void Start()
    {
        MakeD4();
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
                        //print("D4 Face " + (i+1) + " colliding");
                        faceVal = i;
                        //pause then play audio.
                        bell.Toll(0, faceVal + 8);
                        colFlag = false;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //print(collision.gameObject.tag);
        if (collision.collider.tag == "Floor")
        {
            bell.Stop();
            reTrig = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 15)
        {
            bell.Toll(0, faceVal);
        }
        if(other.gameObject.layer == 16)
        {
            bell.Toll(0, faceVal + 4);
        }
    }

    #region D4 Make

    void MakeD4()
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
        face[dir] = faceVerticesD4(dir);
        vertices.AddRange(face[dir]);
        int vCount = vertices.Count;

        triangles.Add(vCount - 3);      // 1 group of 0-2 means 3 total vertices per face
        triangles.Add(vCount - 3 + 1);
        triangles.Add(vCount - 3 + 2);
    }
    #endregion

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
    }
}

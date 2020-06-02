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
    Ray[] faceRays;
    LayerMask mask;



    #region D20 Stats

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
    #endregion



    #region Run

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshRend = GetComponent<MeshRenderer>();
        face = new Vector3[faceCount][];
        faceRays = new Ray[faceCount];
        mask = LayerMask.GetMask("Floor");
    }

    void Start()
    {
        MakeD20();
        UpdateMesh();
        meshCollider.convex = true;
        bell = GetComponent<AudioClipScript>();
    }

    private void Update()
    {
        for (int i = 0; i < faceRays.Length; i++)
        {
            faceRays[i] = new Ray(transform.position, transform.TransformVector(mesh.normals[i * faceVertCount]));
            Debug.DrawLine(transform.position, transform.TransformPoint(mesh.normals[i * faceVertCount]), Color.magenta);
        }

        RaycastHit hitInfo;

        if (Physics.Raycast(faceRays[0], out hitInfo, 2, mask))
        {
            faceVal = 0;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[1], out hitInfo, 2, mask))
        {
            faceVal = 1;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[2], out hitInfo, 2, mask))
        {
            faceVal = 2;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[3], out hitInfo, 2, mask))
        {
            faceVal = 3;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[4], out hitInfo, 2, mask))
        {
            faceVal = 4;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[5], out hitInfo, 2, mask))
        {
            faceVal = 5;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[6], out hitInfo, 2, mask))
        {
            faceVal = 6;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[7], out hitInfo, 2, mask))
        {
            faceVal = 7;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[8], out hitInfo, 2, mask))
        {
            faceVal = 8;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[9], out hitInfo, 2, mask))
        {
            faceVal = 9;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[10], out hitInfo, 2, mask))
        {
            faceVal = 10;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[11], out hitInfo, 2, mask))
        {
            faceVal = 11;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[12], out hitInfo, 2, mask))
        {
            faceVal = 12;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[13], out hitInfo, 2, mask))
        {
            faceVal = 13;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[14], out hitInfo, 2, mask))
        {
            faceVal = 14;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[15], out hitInfo, 2, mask))
        {
            faceVal = 15;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[16], out hitInfo, 2, mask))
        {
            faceVal = 16;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[17], out hitInfo, 2, mask))
        {
            faceVal = 17;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[18], out hitInfo, 2, mask))
        {
            faceVal = 18;
            Debug.Log("D20 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[19], out hitInfo, 2, mask))
        {
            faceVal = 19;
            Debug.Log("D20 faceVal = " + faceVal);
        }
    }
    /*
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
                        //Debug.Log("D20 Face " + (i + 1) + " colliding");
                        faceVal = i;
                        //pause then play audio.
                        //bell.Toll(0, 20 - faceVal);
                        colFlag = false;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.collider.tag == "Floor")
        {
            bell.Stop();
            reTrig = true;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer > 14 && other.gameObject.layer < 31)
        {
            if (other.gameObject.layer == 15) //10
            {
                Debug.Log("Mod 7: " + faceVal % 7);
                bell.Toll(5, faceVal % 7);
            }
            if (other.gameObject.layer == 16) //20
            {
                bell.Toll(6, faceVal);
            }
            if (other.gameObject.layer == 17) //10
            {
                bell.Toll(7, faceVal % 10);
            }
            if (other.gameObject.layer == 18) //20
            {
                bell.Toll(8, faceVal % 10);
            }
            if (other.gameObject.layer == 19) //10
            {
                bell.Toll(9, faceVal % 10);
            }
            if (other.gameObject.layer == 20) //8
            {
                bell.Toll(10, faceVal % 10);
            }
            if (other.gameObject.layer == 21) //20
            {
                bell.Toll(11, faceVal % 10);
            }
            if (other.gameObject.layer == 22) //10
            {
                bell.Toll(12, faceVal % 10);
            }
            if (other.gameObject.layer == 23) //10
            {
                bell.Toll(13, faceVal % 10);
            }
            if (other.gameObject.layer == 24) //10
            {
                bell.Toll(14, faceVal % 10);
            }
            if (other.gameObject.layer == 25) //10
            {
                bell.Toll(15, faceVal);
            }
            if (other.gameObject.layer == 26) //10
            {
                bell.Toll(0, faceVal % 10);
            }
            if (other.gameObject.layer == 27) //10
            {
                bell.Toll(1, faceVal);
            }
            if (other.gameObject.layer == 28) //10
            {
                bell.Toll(2, faceVal % 10);
            }
            if (other.gameObject.layer == 29) //10
            {
                bell.Toll(3, faceVal);
            }
            if (other.gameObject.layer == 30) //20
            {
                bell.Toll(4, faceVal % 10);
            }
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        // needs a condition for when the time sphere is set to reverse
        bell.Toll(other.gameObject.layer - 15, faceVal);
    }
    */
    #endregion



    #region D20 Make
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
    #endregion
}

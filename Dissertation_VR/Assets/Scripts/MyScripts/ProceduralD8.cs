using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD8 : MonoBehaviour
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
    static int faceCount = 8;
    int faceVertCount = 3;
    Ray[] faceRays;
    LayerMask mask;



    #region D8 Stats

    static float C0 = 0.7071067811865475244008443621048f;
    static float C1 = C0 * 2f;

    public static Vector3[] verticesD8 =
    {
        new Vector3(  0,   0,   C1),
        new Vector3(  0,   0,  -C1),
        new Vector3( C1,   0,    0),
        new Vector3(-C1,   0,    0),
        new Vector3(  0,  C1,    0),
        new Vector3(  0, -C1,    0)
    };

    public static int[][] faceTrianglesD8 =
    {
        new int[] { 0, 2, 4 },
        new int[] { 0, 4, 3 },
        new int[] { 0, 3, 5 },
        new int[] { 0, 5, 2 },
        new int[] { 1, 2, 5 },
        new int[] { 1, 5, 3 },
        new int[] { 1, 3, 4 },
        new int[] { 1, 4, 2 }
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
        MakeD8();
        UpdateMesh();
        meshCollider.convex = true;
        bell = GetComponent<AudioClipScript>();
        Debug.Log("Normal length: " + mesh.normals.Length);
    }
    
    private void Update()
    {
        for (int i = 0; i < faceRays.Length; i++)
        {
            faceRays[i] = new Ray(transform.position, transform.TransformVector(mesh.normals[i * faceVertCount]));
            Debug.DrawLine(transform.position, transform.TransformPoint(mesh.normals[i * faceVertCount]), Color.magenta);
        }

        RaycastHit hitInfo;

        if (Physics.Raycast(faceRays[0], out hitInfo, 3, mask))
        {
            faceVal = 0;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[1], out hitInfo, 3, mask))
        {
            faceVal = 1;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[2], out hitInfo, 3, mask))
        {
            faceVal = 2;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[3], out hitInfo, 3, mask))
        {
            faceVal = 3;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[4], out hitInfo, 3, mask))
        {
            faceVal = 4;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[5], out hitInfo, 3, mask))
        {
            faceVal = 5;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[6], out hitInfo, 3, mask))
        {
            faceVal = 6;
            Debug.Log("D8 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[7], out hitInfo, 3, mask))
        {
            faceVal = 7;
            Debug.Log("D8 faceVal = " + faceVal);
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
                        //Debug.Log("D8 Face " + (i + 1) + " colliding");
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
        
            if (other.gameObject.layer == 15)
            {
                bell.Toll(4, faceVal);
            }
            if (other.gameObject.layer == 16)
            {
                bell.Toll(3, faceVal);
            }
        
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TimeSphereScript>().reverse)
        {
            if (other.gameObject.layer == 15)
            {
                bell.Toll(4, faceVal);
            }
            if (other.gameObject.layer == 16)
            {
                bell.Toll(3, faceVal);
            }
        }
    }
    */
    #endregion



    #region D8 Make

    void MakeD8()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < faceCount; i++)
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        face[dir] = faceVerticesD8(dir);
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
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD6 : MonoBehaviour
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
    static int faceCount = 6;
    int faceVertCount = 4;
    Ray[] faceRays;
    LayerMask mask;



    #region D6 Stats

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
        MakeD6();
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
            Debug.Log("D6 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[1], out hitInfo, 3, mask))
        {
            faceVal = 1;
            Debug.Log("D6 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[2], out hitInfo, 3, mask))
        {
            faceVal = 2;
            Debug.Log("D6 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[3], out hitInfo, 3, mask))
        {
            faceVal = 3;
            Debug.Log("D6 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[4], out hitInfo, 3, mask))
        {
            faceVal = 4;
            Debug.Log("D6 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[5], out hitInfo, 3, mask))
        {
            faceVal = 5;
            Debug.Log("D6 faceVal = " + faceVal);
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
                string col4 = collision.GetContact(3).point.ToString();

                for (int i = 0; i < faceCount; i++)
                {
                    if (globalFace[i].Contains(col1) && globalFace[i].Contains(col2) && globalFace[i].Contains(col3) && globalFace[i].Contains(col4))
                    {
                        //Debug.Log("D6 Face " + (i + 1) + " colliding");
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
        if (other.gameObject.layer > 14 && other.gameObject.layer < 31)
        {
            if (other.gameObject.layer == 15) //10
            {
                bell.Toll(1, faceVal);
            }
            if (other.gameObject.layer == 16) //20
            {
                bell.Toll(2, faceVal);
            }
            if (other.gameObject.layer == 17) //10
            {
                bell.Toll(3, faceVal);
            }
            if (other.gameObject.layer == 18) //20
            {
                bell.Toll(4, faceVal);
            }
            if (other.gameObject.layer == 19) //10
            {
                bell.Toll(5, faceVal);
            }
            if (other.gameObject.layer == 20) //8
            {
                bell.Toll(6, faceVal);
            }
            if (other.gameObject.layer == 21) //20
            {
                bell.Toll(7, faceVal);
            }
            if (other.gameObject.layer == 22) //10
            {
                bell.Toll(8, faceVal);
            }
            if (other.gameObject.layer == 23) //10
            {
                bell.Toll(9, faceVal);
            }
            if (other.gameObject.layer == 24) //10
            {
                bell.Toll(10, faceVal);
            }
            if (other.gameObject.layer == 25) //10
            {
                bell.Toll(11, faceVal);
            }
            if (other.gameObject.layer == 26) //10
            {
                bell.Toll(12, faceVal);
            }
            if (other.gameObject.layer == 27) //10
            {
                bell.Toll(13, faceVal);
            }
            if (other.gameObject.layer == 28) //10
            {
                bell.Toll(14, faceVal);
            }
            if (other.gameObject.layer == 29) //10
            {
                bell.Toll(15, faceVal);
            }
            if (other.gameObject.layer == 30) //20
            {
                bell.Toll(0, faceVal);
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



    #region D6 Make

    void MakeD6()
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
        face[dir] = faceVerticesD6(dir);
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
    #endregion
}
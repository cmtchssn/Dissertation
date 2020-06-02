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
    Ray[] faceRays;
    LayerMask mask;



    #region D4 Stats

    static float C0 = 0.353553390593273762200422181052f;// = Mathf.Sqrt(2f) / 4f;
    static float C1 = C0 * 2f;

    public static Vector3[] verticesD4 =      // where each face connects in space
    {
        new Vector3( C1, -C1,  C1),
        new Vector3( C1,  C1, -C1),
        new Vector3(-C1,  C1,  C1),
        new Vector3(-C1, -C1, -C1)
    };

    public static int[][] faceTrianglesD4 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] { 0, 1, 2 },
        new int[] { 1, 0, 3 },
        new int[] { 2, 3, 0 },
        new int[] { 3, 2, 1 }
    };

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
        MakeD4();
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

        if (Physics.Raycast(faceRays[0], out hitInfo, 1, mask))
        {
            faceVal = 0;
            //Debug.Log("D4 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[1], out hitInfo, 1, mask))
        {
            faceVal = 1;
            //Debug.Log("D4 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[2], out hitInfo, 1, mask))
        {
            faceVal = 2;
            //Debug.Log("D4 faceVal = " + faceVal);
        }
        else if (Physics.Raycast(faceRays[3], out hitInfo, 1, mask))
        {
            faceVal = 3;
            //Debug.Log("D4 faceVal = " + faceVal);
        }

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("normals: " + mesh.normals[9] + ", " + mesh.normals[10] + ", " + mesh.normals[11] + ", ");
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
            //Get global positions of each face vertex, to string, transform them to local position for later comparisons to determine what face is touching the floor.
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
                        //Debug.Log("D4 Face " + (i+1) + " colliding");
                        faceVal = i;
                        //pause then play audio.
                        //bell.Toll(0, faceVal + 8);
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
                bell.Toll(0, faceVal);
            }
            if (other.gameObject.layer == 16) //20
            {
                bell.Toll(1, faceVal);
            }
            if (other.gameObject.layer == 17) //10
            {
                bell.Toll(2, faceVal);
            }
            if (other.gameObject.layer == 18) //20
            {
                bell.Toll(3, faceVal);
            }
            if (other.gameObject.layer == 19) //10
            {
                bell.Toll(4, faceVal);
            }
            if (other.gameObject.layer == 20) //8
            {
                bell.Toll(5, faceVal);
            }
            if (other.gameObject.layer == 21) //20
            {
                bell.Toll(6, faceVal);
            }
            if (other.gameObject.layer == 22) //10
            {
                bell.Toll(7, faceVal);
            }
            if (other.gameObject.layer == 23) //10
            {
                bell.Toll(8, faceVal);
            }
            if (other.gameObject.layer == 24) //10
            {
                bell.Toll(9, faceVal);
            }
            if (other.gameObject.layer == 25) //10
            {
                bell.Toll(10, faceVal);
            }
            if (other.gameObject.layer == 26) //10
            {
                bell.Toll(11, faceVal);
            }
            if (other.gameObject.layer == 27) //10
            {
                bell.Toll(12, faceVal);
            }
            if (other.gameObject.layer == 28) //10
            {
                bell.Toll(13, faceVal);
            }
            if (other.gameObject.layer == 29) //10
            {
                bell.Toll(14, faceVal);
            }
            if (other.gameObject.layer == 30) //20
            {
                bell.Toll(15, faceVal);
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

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        //Debug.Log(mesh.triangles[0] + ", " + mesh.triangles[1] + ", " + mesh.triangles[2] + ", " + mesh.triangles[3]);
        mesh.RecalculateNormals();
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
    }
    #endregion
}

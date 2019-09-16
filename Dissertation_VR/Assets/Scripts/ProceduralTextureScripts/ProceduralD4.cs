using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ProceduralD4 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;
    RaycastHit hit;

    Vector3[] face1 = faceVerticesD4(0);
    Vector3[] face2 = faceVerticesD4(1);
    Vector3[] face3 = faceVerticesD4(2);
    Vector3[] face4 = faceVerticesD4(3);

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

    public static Vector3[] faceVerticesD4(int dir)
    {
        Vector3[] fv = new Vector3[3]; // number of vertices per face
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD4[faceTrianglesD4[dir][i]];
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

    void Start()
    {
        MakeD4();
        UpdateMesh();
        meshCollider.convex = true;
        //Debug.Log();
    }

    private void Update()
    {
        //print(meshCollider.transform.rotation);
    }

    void OnCollisionStay(Collision collision)
    {
        string gface1p1 = transform.TransformPoint(face1[0]).ToString();
        string gface1p2 = transform.TransformPoint(face1[1]).ToString();
        string gface1p3 = transform.TransformPoint(face1[2]).ToString();
        
        string gface2p1 = transform.TransformPoint(face2[0]).ToString();
        string gface2p2 = transform.TransformPoint(face2[1]).ToString();
        string gface2p3 = transform.TransformPoint(face2[2]).ToString();
        
        string gface3p1 = transform.TransformPoint(face3[0]).ToString();
        string gface3p2 = transform.TransformPoint(face3[1]).ToString();
        string gface3p3 = transform.TransformPoint(face3[2]).ToString();
        
        string gface4p1 = transform.TransformPoint(face4[0]).ToString();
        string gface4p2 = transform.TransformPoint(face4[1]).ToString();
        string gface4p3 = transform.TransformPoint(face4[2]).ToString();

        Debug.Log(collision.GetContact(0).point.ToString() + collision.GetContact(1).point.ToString() + collision.GetContact(2).point.ToString() + "3 contact points");
        Debug.Log(gface1p1 + gface1p2 + gface1p3 + " f1 transform points");
        Debug.Log(gface2p1 + gface2p2 + gface2p3 + " f2 transform points");
        Debug.Log(gface3p1 + gface3p2 + gface3p3 + " f3 transform points");
        Debug.Log(gface4p1 + gface4p2 + gface4p3 + " f4 transform points");
        
        if(collision.contactCount == 3)
        {
            if(collision.GetContact(0).ToString() == gface1p1)
            {
            }
        }
    }

    void MakeD4()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 4; i++)     // i < number of faces shape has
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        vertices.AddRange(faceVerticesD4(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 3);      // 1 group of 0-2 means 3 total vertices per face
        triangles.Add(vCount - 3 + 1);
        triangles.Add(vCount - 3 + 2);

        // Can I name this or give it some attribute?
        //Debug.Log("Face " + triangles[0].ToString() + " has been created.");
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

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
        print(meshCollider.transform.rotation);
        print(mesh.normals[0].ToString() + mesh.normals[1].ToString() + mesh.normals[2].ToString() + mesh.normals[3].ToString());
    }

    void OnCollisionStay(Collision collision)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
            //Debug.Log(mesh.vertices[0].ToString() + ", " + mesh.vertices[1].ToString() + ", " + mesh.vertices[2].ToString() + ", " + mesh.vertices[3].ToString());
        }

        // Which 3 verticesD4[] are touching something?
        // Is that something the floor?
        // verticesD4[i],[j],[k] make face Fijk
        // Check normals of face?
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

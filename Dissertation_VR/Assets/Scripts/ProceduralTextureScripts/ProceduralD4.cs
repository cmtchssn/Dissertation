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
        MakeD4();
        UpdateMesh();
        meshCollider.convex = true;
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        //Debug.Log(collision.contactCount);
        //Debug.Log(collision.contacts);
    }
    
    void OnCollisionStay(Collision collisionInfo)
    {
        ContactPoint[] contactPts = new ContactPoint[4];
        Debug.Log(collisionInfo.GetContacts(contactPts));
        //Can I return the vertices of the contact points?
        //When there are three contact points (1 face for this shape) return the vertices of the three points
    }
    */
    void MakeD4()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 4; i++)     // i < number of faces shape has
        {
            MakeFace(i);
        }
        Debug.Log(vertices[0]);
        Debug.Log(vertices[1]);
        Debug.Log(vertices[2]);
        Debug.Log(vertices[3]);
        Debug.Log(vertices[4]);
        Debug.Log(vertices[5]);
        Debug.Log(vertices.Count);
        //Debug.Log(triangles[0] + triangles[1] + triangles[2] + triangles[3]);
    }

    void MakeFace(int dir)
    {
        vertices.AddRange(D4MeshData.faceVertices(dir));
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

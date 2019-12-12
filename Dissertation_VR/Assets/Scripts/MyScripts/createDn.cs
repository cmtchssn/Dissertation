using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createDn : MonoBehaviour
{
    Dn dn;
    GameObject mt;
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;

    private void Awake()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            makeDnObject(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dn = new Dn(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dn = new Dn(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            dn = new Dn(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            dn = new Dn(12);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            dn = new Dn(20);
        }
    }

    public void makeDnObject(int face)
    {
        string name = face.ToString();
        mt = new GameObject("D" + name);
        mt.transform.position = new Vector3(0, 1, 0);

        mt.AddComponent<MeshFilter>();
        mt.AddComponent<MeshCollider>();
        mt.AddComponent<MeshRenderer>();

        dn = new Dn(face);
        UpdateMesh();
        mesh = mt.GetComponent<MeshFilter>().mesh;
        meshCollider = mt.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshRend = mt.GetComponent<MeshRenderer>();

        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = dn.makeVertices.ToArray();
        mesh.triangles = dn.makeTriangles.ToArray();
        mesh.RecalculateNormals();
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
        meshCollider.convex = true;
    }
}

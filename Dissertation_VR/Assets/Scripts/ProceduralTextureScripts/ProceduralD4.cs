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

    AudioSource audioSource;
    //AudioClip[] clipArray;// = (Resources.Load("guitarChordsD4-01.wav") as AudioClip, Resources.Load("guitarChordsD4-02.wav") as AudioClip, Resources.Load("guitarChordsD4-03.wav") as AudioClip, Resources.Load("guitarChordsD4-04.wav") as AudioClip);
    //AudioClip clip;

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
        audioSource = GetComponent<AudioSource>();
        //clip = GetComponent<AudioSource>().clip;
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

        string col1 = collision.GetContact(0).point.ToString();
        string col2 = collision.GetContact(1).point.ToString();
        string col3 = collision.GetContact(2).point.ToString();

        string f1 = gface1p1 + gface1p2 + gface1p3;
        string f2 = gface2p1 + gface2p2 + gface2p3;
        string f3 = gface3p1 + gface3p2 + gface3p3;
        string f4 = gface4p1 + gface4p2 + gface4p3;

        if(collision.contactCount == 3)
        {
            if (f1.Contains(col1) && f1.Contains(col2) && f1.Contains(col3))
            {
                print("Face 1 colliding");
                audioSource.Pause();
                audioSource.clip = Resources.Load("guitarChordsD4-01") as AudioClip;
                audioSource.Play();
                //pause then play audio.
            }
            else if (f2.Contains(col1) && f2.Contains(col2) && f2.Contains(col3))
            {
                print("Face 2 colliding");
                audioSource.Pause();
                audioSource.clip = Resources.Load("guitarChordsD4-02") as AudioClip;
                audioSource.Play();
                //pause then play audio.
            }
            else if (f3.Contains(col1) && f3.Contains(col2) && f3.Contains(col3))
            {
                print("Face 3 colliding");
                audioSource.Pause();
                audioSource.clip = Resources.Load("guitarChordsD4-03") as AudioClip;
                audioSource.Play();
                //pause then play audio.
            }
            else if (f4.Contains(col1) && f4.Contains(col2) && f4.Contains(col3))
            {
                print("Face 4 colliding");
                audioSource.Pause();
                audioSource.clip = Resources.Load("guitarChordsD4-04") as AudioClip;
                audioSource.Play();
                //pause then play audio.
            }
        }
        else
        {
            audioSource.Pause();
            //pause audio
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

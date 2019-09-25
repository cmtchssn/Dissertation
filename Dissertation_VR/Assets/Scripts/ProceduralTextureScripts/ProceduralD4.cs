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

    AudioSource audioSource;
    Vector3[][] face;
    static int faceCount = 4;
    int faceVertCount = 3;
    string[] clipNames = new string[] { "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04" };
    
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
        face = new Vector3[faceCount][];
    }

    void Start()
    {
        MakeD4();
        UpdateMesh();
        meshCollider.convex = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
    }

    void OnCollisionStay(Collision collision)
    {
        string[] globalFace = new string[faceCount];
        for (int i = 0; i < faceCount; i++)
        {
            for (int j= 0; j < faceVertCount; j++)
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
                if(globalFace[i].Contains(col1) && globalFace[i].Contains(col2) && globalFace[i].Contains(col3))
                {
                    //print("D4 Face " + (i+1) + " colliding");
                    audioSource.Stop();
                    audioSource.clip = Resources.Load(clipNames[i]) as AudioClip;
                    audioSource.Play();
                    //pause then play audio.
                }
            }
        }
        else
        {
            audioSource.Stop();
            //pause audio
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        audioSource.Stop();
    }

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
        mesh.RecalculateNormals();
        meshRend.material = Resources.Load("shapePrototypingMaterial") as Material;
    }
}

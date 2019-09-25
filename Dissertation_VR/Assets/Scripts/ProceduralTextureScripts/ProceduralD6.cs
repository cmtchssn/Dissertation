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

    AudioSource audioSource;
    Vector3[][] face;
    static int faceCount = 6;
    int faceVertCount = 4;
    string[] clipNames = new string[] { "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04", "guitarChordsD4-03", "guitarChordsD4-02" };

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
        MakeD6();
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
                    //print("D6 Face " + (i + 1) + " colliding");
                    audioSource.Pause();
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
}

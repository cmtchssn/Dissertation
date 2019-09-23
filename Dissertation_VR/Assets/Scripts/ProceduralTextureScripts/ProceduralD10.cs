using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD10 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;

    AudioSource audioSource;
    Vector3[][] face;
    static int faceCount = 10;
    int faceVertCount = 4;
    string[] clipNames = new string[] { "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04", "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04", "guitarChordsD4-03", "guitarChordsD4-04" };

    static float C0 = 0.309016994374947424102293417183f;// = (Mathf.Sqrt(5f) - 1f) / 4f;
    static float C1 = 0.809016994374947424102293417183f;// (1f + Mathf.Sqrt(5f)) / 4f;
    static float C2 = 1.30901699437494742410229341718f;// (3f + Mathf.Sqrt(5f)) / 4f;

    public static Vector3[] verticesD10 =
    {
        new Vector3( 0.0f,    C0,    C1),
        new Vector3( 0.0f,    C0,   -C1),
        new Vector3( 0.0f,   -C0,    C1),
        new Vector3( 0.0f,   -C0,   -C1),
        new Vector3( 0.5f,  0.5f,  0.5f),
        new Vector3( 0.5f,  0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f,  0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(   C2,   -C1,  0.0f),
        new Vector3(  -C2,    C1,  0.0f),
        new Vector3(   C0,    C1,  0.0f),
        new Vector3(  -C0,   -C1,  0.0f)

    };

    public static int[][] faceTrianglesD10 =
    {
        //half of the faces are inverse, have fun troubleshooting
        new int[] { 8,  2,  6, 11 },
        new int[] { 8, 11,  7,  3 },
        new int[] { 8,  3,  1,  5 },
        new int[] { 8,  5, 10,  4 },
        new int[] { 8,  4,  0,  2 },
        new int[] { 9,  0,  4, 10 },
        new int[] { 9, 10,  5,  1 },
        new int[] { 9,  1,  3,  7 },
        new int[] { 9,  7, 11,  6 },
        new int[] { 9,  6,  2,  0 }   //copied this from dmccooey
    };

    public static Vector3[] faceVerticesD10(int dir)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD10[faceTrianglesD10[dir][i]];
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
        MakeD10();
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
                    //print("D10 Face " + (i + 1) + " colliding");
                    audioSource.Pause();
                    audioSource.clip = Resources.Load(clipNames[i]) as AudioClip;
                    audioSource.Play();
                    //pause then play audio.
                }
            }
        }
        else
        {
            audioSource.Pause();
            //pause audio
        }
    }

    void MakeD10()
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
        face[dir] = faceVerticesD10(dir);
        vertices.AddRange(faceVerticesD10(dir));
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

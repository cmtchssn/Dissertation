using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class ProceduralD8 : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshRenderer meshRend;
    List<Vector3> vertices;
    List<int> triangles;
    bool colFlag = false;
    bool reTrig = true;

    AudioSource audioSource;
    Vector3[][] face;
    static int faceCount = 8;
    int faceVertCount = 3;
    string[] clipNames = new string[] { "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04", "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04" };

    public static Vector3[] verticesD8 =
    {
        new Vector3( 0,  1,  0),
        new Vector3( 1,  0,  1),
        new Vector3(-1,  0,  1),
        new Vector3(-1,  0, -1),
        new Vector3( 1,  0, -1),
        new Vector3( 0, -1,  0)
    };

    public static int[][] faceTrianglesD8 =
    {
        //top triangles
        new int[] { 2, 1, 0 },
        new int[] { 3, 2, 0 },
        new int[] { 4, 3, 0 },
        new int[] { 1, 4, 0 },
        //bottom triangles
        new int[] { 1, 2, 5 },
        new int[] { 2, 3, 5 },
        new int[] { 3, 4, 5 },
        new int[] { 4, 1, 5 }   //culling is weird on this one, these are counterclockwise, but work.
    };

    public static Vector3[] faceVerticesD8(int dir)
    {
        Vector3[] fv = new Vector3[3];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD8[faceTrianglesD8[dir][i]];
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
        MakeD8();
        UpdateMesh();
        meshCollider.convex = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;
    }

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

                for (int i = 0; i < faceCount; i++)
                {
                    if (globalFace[i].Contains(col1) && globalFace[i].Contains(col2) && globalFace[i].Contains(col3))
                    {
                        //print("D8 Face " + (i + 1) + " colliding");
                        audioSource.Pause();
                        audioSource.clip = Resources.Load(clipNames[i]) as AudioClip;
                        audioSource.Play();
                        colFlag = false;
                        //pause then play audio.
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        print(collision.gameObject.tag);
        if (collision.collider.tag == "Floor")
        {
            audioSource.Stop();
            reTrig = true;
        }
    }

    void MakeD8()
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
        face[dir] = faceVerticesD8(dir);
        vertices.AddRange(faceVerticesD8(dir));
        int vCount = vertices.Count;

        triangles.Add(vCount - 3);
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

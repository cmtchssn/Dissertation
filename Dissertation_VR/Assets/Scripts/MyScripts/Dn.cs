using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dn
{
    Vector3[] vertices;// = new Vector3[20]; //maybe I can use this in faceVertices
    int[][] faceTriangles;// = new int[20][];
    List<Vector3> makeVertices;
    List<int> makeTriangles;
    Vector3[][] face;
    int faceVertCount;
    int faceSwitch;

    public int FaceCount { get; set; }
    public Dn(int faceNum)
    {
        FaceCount = faceNum;
        faceSwitch = FaceCount;
        switch (faceSwitch)
        {
            case 4:
                vertices = verticesD4;
                faceTriangles = faceTrianglesD4;
                break;
            case 6:
                vertices = verticesD6;
                faceTriangles = faceTrianglesD6;
                break;
            case 8:
                vertices = verticesD8;
                faceTriangles = faceTrianglesD8;
                break;
            case 10:
                vertices = verticesD10;
                faceTriangles = faceTrianglesD10;
                break;
            case 12:
                vertices = verticesD12;
                faceTriangles = faceTrianglesD12;
                break;
            case 20:
                vertices = verticesD20;
                faceTriangles = faceTrianglesD20;
                break;
        }

    }

    #region D4 Stats
    static float d4a = 0.353553390593273762200422181052f;// = Mathf.Sqrt(2f) / 4f;
    static float d4b = d4a * 2f;

    public static Vector3[] verticesD4 =      // where each face connects in space
    {
        new Vector3( d4b, -d4b,  d4b),
        new Vector3( d4b,  d4b, -d4b),
        new Vector3(-d4b,  d4b,  d4b),
        new Vector3(-d4b, -d4b, -d4b)
    };

    public static int[][] faceTrianglesD4 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] { 0, 1, 2 },
        new int[] { 1, 0, 3 },
        new int[] { 2, 3, 0 },
        new int[] { 3, 2, 1 }
    };
    #endregion

    #region D6 Stats
    static float d6a = 1f;

    public static Vector3[] verticesD6 =
    {
        new Vector3( d6a,  d6a,  d6a),
        new Vector3( d6a,  d6a, -d6a),
        new Vector3( d6a, -d6a,  d6a),
        new Vector3( d6a, -d6a, -d6a),
        new Vector3(-d6a,  d6a,  d6a),
        new Vector3(-d6a,  d6a, -d6a),
        new Vector3(-d6a, -d6a,  d6a),
        new Vector3(-d6a, -d6a, -d6a)
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

    #endregion

    #region D8 Stats
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
    #endregion

    #region D10 Stats
    static float d10a = 0.309016994374947424102293417183f;// = (Mathf.Sqrt(5f) - 1f) / 4f;
    static float d10b = 0.809016994374947424102293417183f;// (1f + Mathf.Sqrt(5f)) / 4f;
    static float d10c = 1.30901699437494742410229341718f;// (3f + Mathf.Sqrt(5f)) / 4f;

    public static Vector3[] verticesD10 =
    {
        new Vector3( 0.0f,  d10a,  d10b),
        new Vector3( 0.0f,  d10a, -d10b),
        new Vector3( 0.0f, -d10a,  d10b),
        new Vector3( 0.0f, -d10a, -d10b),
        new Vector3( 0.5f,  0.5f,  0.5f),
        new Vector3( 0.5f,  0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f,  0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3( d10c, -d10b,  0.0f),
        new Vector3(-d10c,  d10b,  0.0f),
        new Vector3( d10a,  d10b,  0.0f),
        new Vector3(-d10a, -d10b,  0.0f)

    };

    public static int[][] faceTrianglesD10 =
    {
        new int[] { 8,  2,  6, 11 },
        new int[] { 8, 11,  7,  3 },
        new int[] { 8,  3,  1,  5 },
        new int[] { 8,  5, 10,  4 },
        new int[] { 8,  4,  0,  2 },
        new int[] { 9,  0,  4, 10 },
        new int[] { 9, 10,  5,  1 },
        new int[] { 9,  1,  3,  7 },
        new int[] { 9,  7, 11,  6 },
        new int[] { 9,  6,  2,  0 }  
    };
    #endregion

    #region D12 Stats
    static float d12a = 0.809016994374947424102293417183f;    // = (1f + Math.Sqrt(5f)) / 4f
    static float d12b = 1.30901699437494742410229341718f;     // = (3f + Math.Sqrt(5f)) / 4f

    public static Vector3[] verticesD12 =      // where each face connects in space
    {
        new Vector3( 0.0f,  0.5f,  d12b),
        new Vector3( 0.0f,  0.5f, -d12b),
        new Vector3( 0.0f, -0.5f,  d12b),
        new Vector3( 0.0f, -0.5f, -d12b),
        new Vector3( d12b,  0.0f,  0.5f),
        new Vector3( d12b,  0.0f, -0.5f),
        new Vector3(-d12b,  0.0f,  0.5f),
        new Vector3(-d12b,  0.0f, -0.5f),
        new Vector3( 0.5f,  d12b,  0.0f),
        new Vector3( 0.5f, -d12b,  0.0f),
        new Vector3(-0.5f,  d12b,  0.0f),
        new Vector3(-0.5f, -d12b,  0.0f),
        new Vector3( d12a,  d12a,  d12a),
        new Vector3( d12a,  d12a, -d12a),
        new Vector3( d12a, -d12a,  d12a),
        new Vector3( d12a, -d12a, -d12a),
        new Vector3(-d12a,  d12a,  d12a),
        new Vector3(-d12a,  d12a, -d12a),
        new Vector3(-d12a, -d12a,  d12a),
        new Vector3(-d12a, -d12a, -d12a)
    };

    public static int[][] faceTrianglesD12 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] {  0,  2, 14,  4, 12 },
        new int[] {  0, 12,  8, 10, 16 },
        new int[] {  0, 16,  6, 18,  2 },
        new int[] {  7,  6, 16, 10, 17 },
        new int[] {  7, 17,  1,  3, 19 },
        new int[] {  7, 19, 11, 18,  6 },
        new int[] {  9, 11, 19,  3, 15 },
        new int[] {  9, 15,  5,  4, 14 },
        new int[] {  9, 14,  2, 18, 11 },
        new int[] { 13,  1, 17, 10,  8 },
        new int[] { 13,  8, 12,  4,  5 },
        new int[] { 13,  5, 15,  3,  1 }
    };
    #endregion

    #region D20 Stats
    static float d20a = 0.809016994374947424102293417183f;    // = (1f + Mathf.Sqrt(5f)) / 4f;

    public static Vector3[] verticesD20 =      // where each face connects in space
    {
        new Vector3( 0.5f,  0.0f,  d20a),
        new Vector3( 0.5f,  0.0f, -d20a),
        new Vector3(-0.5f,  0.0f,  d20a),
        new Vector3(-0.5f,  0.0f, -d20a),
        new Vector3( d20a,  0.5f,  0.0f),
        new Vector3( d20a, -0.5f,  0.0f),
        new Vector3(-d20a,  0.5f,  0.0f),
        new Vector3(-d20a, -0.5f,  0.0f),
        new Vector3( 0.0f,  d20a,  0.5f),
        new Vector3( 0.0f,  d20a, -0.5f),
        new Vector3( 0.0f, -d20a,  0.5f),
        new Vector3( 0.0f, -d20a, -0.5f)
    };

    public static int[][] faceTrianglesD20 =   // what order to connect vertices to create an outward facing mesh for each face
    {
        new int[] {  0,  2, 10 },
        new int[] {  0, 10,  5 },
        new int[] {  0,  5,  4 },
        new int[] {  0,  4,  8 },
        new int[] {  0,  8,  2 },
        new int[] {  3,  1, 11 },
        new int[] {  3, 11,  7 },
        new int[] {  3,  7,  6 },
        new int[] {  3,  6,  9 },
        new int[] {  3,  9,  1 },
        new int[] {  2,  6,  7 },
        new int[] {  2,  7, 10 },
        new int[] { 10,  7, 11 },
        new int[] { 10, 11,  5 },
        new int[] {  5, 11,  1 },
        new int[] {  5,  1,  4 },
        new int[] {  4,  1,  9 },
        new int[] {  4,  9,  8 },
        new int[] {  8,  9,  6 },
        new int[] {  8,  6,  2 }
    };
    #endregion

    public Vector3[] faceVertices(int dir)
    {
        faceVertCount = faceTriangles.Length;
        Vector3[] fv = new Vector3[faceVertCount]; // number of vertices per face

        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[faceTriangles[dir][i]];
        }

        return fv;
    }

    void Make()
    {
        makeVertices = new List<Vector3>();
        makeTriangles = new List<int>();

        for (int i = 0; i < FaceCount; i++)     // i < number of faces shape has
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        face[dir] = faceVertices(dir);
        makeVertices.AddRange(face[dir]);
        int vCount = makeVertices.Count;
        
        switch (faceSwitch)
        {
            case 4:
                makeTriangles.Add(vCount - 3);
                makeTriangles.Add(vCount - 3 + 1);
                makeTriangles.Add(vCount - 3 + 2);
                break;
            case 6:
                makeTriangles.Add(vCount - 4);
                makeTriangles.Add(vCount - 4 + 1);
                makeTriangles.Add(vCount - 4 + 2);
                makeTriangles.Add(vCount - 4);
                makeTriangles.Add(vCount - 4 + 2);
                makeTriangles.Add(vCount - 4 + 3);
                break;
            case 8:
                makeTriangles.Add(vCount - 3);
                makeTriangles.Add(vCount - 3 + 1);
                makeTriangles.Add(vCount - 3 + 2);
                break;
            case 10:
                makeTriangles.Add(vCount - 4);
                makeTriangles.Add(vCount - 4 + 1);
                makeTriangles.Add(vCount - 4 + 2);
                makeTriangles.Add(vCount - 4);
                makeTriangles.Add(vCount - 4 + 2);
                makeTriangles.Add(vCount - 4 + 3);
                break;
            case 12:
                makeTriangles.Add(vCount - 5);
                makeTriangles.Add(vCount - 5 + 1);
                makeTriangles.Add(vCount - 5 + 2);
                makeTriangles.Add(vCount - 5);
                makeTriangles.Add(vCount - 5 + 2);
                makeTriangles.Add(vCount - 5 + 3);
                makeTriangles.Add(vCount - 5);
                makeTriangles.Add(vCount - 5 + 3);
                makeTriangles.Add(vCount - 5 + 4);
                break;
            case 20:
                makeTriangles.Add(vCount - 3);
                makeTriangles.Add(vCount - 3 + 1);
                makeTriangles.Add(vCount - 3 + 2);
                break;
        }
        
    }
}

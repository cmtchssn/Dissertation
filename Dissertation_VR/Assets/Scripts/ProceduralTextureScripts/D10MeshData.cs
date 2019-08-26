
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class D10MeshData
{
    static float C0 = 0.309016994374947424102293417183f;// = (Mathf.Sqrt(5f) - 1f) / 4f;
    static float C1 = 0.809016994374947424102293417183f;// (1f + Mathf.Sqrt(5f)) / 4f;
    static float C2 = 1.30901699437494742410229341718f;// (3f + Mathf.Sqrt(5f)) / 4f;

    public static Vector3[] vertices =
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

    public static int[][] faceTriangles =
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

    public static Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[faceTriangles[dir][i]];
        }
        return fv;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class D12MeshData
{
    static float C0 = 0.809016994374947424102293417183f;    // = (1f + Math.Sqrt(5f)) / 4f
    static float C1 = 1.30901699437494742410229341718f;     // = (3f + Math.Sqrt(5f)) / 4f

    public static Vector3[] vertices =      // where each face connects in space
    {
        new Vector3( 0.0f,  0.5f,    C1),
        new Vector3( 0.0f,  0.5f,   -C1),
        new Vector3( 0.0f, -0.5f,    C1),
        new Vector3( 0.0f, -0.5f,   -C1),
        new Vector3(   C1,  0.0f,  0.5f),
        new Vector3(   C1,  0.0f, -0.5f),
        new Vector3(  -C1,  0.0f,  0.5f),
        new Vector3(  -C1,  0.0f, -0.5f),
        new Vector3( 0.5f,    C1,  0.0f),
        new Vector3( 0.5f,   -C1,  0.0f),
        new Vector3(-0.5f,    C1,  0.0f),
        new Vector3(-0.5f,   -C1,  0.0f),
        new Vector3(   C0,    C0,    C0),
        new Vector3(   C0,    C0,   -C0),
        new Vector3(   C0,   -C0,    C0),
        new Vector3(   C0,   -C0,   -C0),
        new Vector3(  -C0,    C0,    C0),
        new Vector3(  -C0,    C0,   -C0),
        new Vector3(  -C0,   -C0,    C0),
        new Vector3(  -C0,   -C0,   -C0)
    };

    public static int[][] faceTriangles =   // what order to connect vertices to create an outward facing mesh for each face
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

    public static Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[5]; // number of vertices per face
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[faceTriangles[dir][i]];
        }
        return fv;
    }
}

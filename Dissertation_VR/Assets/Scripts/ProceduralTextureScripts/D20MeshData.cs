
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class D20MeshData
{
    /*
    static float C0 = 0.809016994374947424102293417183f;    // = (1f + Mathf.Sqrt(5f)) / 4f;

    public static Vector3[] verticesD20 =      // where each face connects in space
    {
        new Vector3( 0.5f,  0.0f,    C0),
        new Vector3( 0.5f,  0.0f,   -C0),
        new Vector3(-0.5f,  0.0f,    C0),
        new Vector3(-0.5f,  0.0f,   -C0),
        new Vector3(   C0,  0.5f,  0.0f),
        new Vector3(   C0, -0.5f,  0.0f),
        new Vector3(  -C0,  0.5f,  0.0f),
        new Vector3(  -C0, -0.5f,  0.0f),
        new Vector3( 0.0f,    C0,  0.5f),
        new Vector3( 0.0f,    C0, -0.5f),
        new Vector3( 0.0f,   -C0,  0.5f),
        new Vector3( 0.0f,   -C0, -0.5f)
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

    public static Vector3[] faceVerticesD20(int dir)
    {
        Vector3[] fv = new Vector3[3]; // number of vertices per face
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = verticesD20[faceTrianglesD20[dir][i]];
        }
        return fv;
    }
    */
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class D4MeshData
{
    /*
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
    */
}

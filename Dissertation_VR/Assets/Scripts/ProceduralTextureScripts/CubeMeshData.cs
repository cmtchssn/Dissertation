    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData
{
    public static Vector3[] vertices =
    {
        //north face vertices
        new Vector3( 1,  1,  1),
        new Vector3(-1,  1,  1),
        new Vector3(-1, -1,  1),
        new Vector3( 1, -1,  1),
        //south face vertices
        new Vector3(-1,  1, -1),
        new Vector3( 1,  1, -1),
        new Vector3( 1, -1, -1),
        new Vector3(-1, -1, -1)
    };

    public static int[][] faceTriangles =
    {
        //north face
        new int[] { 0, 1, 2, 3 },
        //east face
        new int[] { 5, 0, 3, 6 },
        //south face
        new int[] { 4, 5, 6, 7 },
        //west face
        new int[] { 1, 4, 7, 2 },
        //top face
        new int[] { 5, 4, 1, 0 },
        //bottom face
        new int[] { 3, 2, 7, 6 } //counterclockwise for culling purposes, i think
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

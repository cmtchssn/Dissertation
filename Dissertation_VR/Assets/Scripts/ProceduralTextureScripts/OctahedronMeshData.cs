﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OctahedronMeshData
{
    public static Vector3[] vertices =
    {
        new Vector3(0, 1, 0),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(1, 0, -1),
        new Vector3(0, -1, 0)
    };

    public static int[][] faceTriangles =
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

    public static Vector3[] faceVertices(int dir)
    {
        Vector3[] fv = new Vector3[3];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = vertices[faceTriangles[dir][i]];
        }
        return fv;
    }
}

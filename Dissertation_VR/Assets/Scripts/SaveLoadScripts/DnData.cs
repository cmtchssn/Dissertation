using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DnData
{
    public string name;
    public float[] position;
    public GameObject[] children;
    public string[] babyNames;
    public float[][] babyPos;

    public DnData(DnParent mama)
    {
        int kids = mama.transform.childCount;
        GameObject[] children = new GameObject[kids];
        string[] babyNames = new string[kids];
        float[][] babyPos = new float[kids][];
        for (int i = 0; i < kids; i++)
        {
            babyNames[i] = children[i].name;
            // I need to set children to an instance of an existing prefab object
            babyPos[i][0] = children[i].transform.position.x;
            babyPos[i][1] = children[i].transform.position.y;
            babyPos[i][2] = children[i].transform.position.z;
        }

        name = mama.name;

        position = new float[3];
        position[0] = mama.transform.position.x;
        position[1] = mama.transform.position.y;
        position[2] = mama.transform.position.z;
    }

}

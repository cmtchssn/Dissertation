﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DnParent : MonoBehaviour
{
    public void SaveState()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadState()
    {
        DnData data = SaveSystem.LoadData();
        this.name = data.name;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        GameObject[] babies = new GameObject[data.children.Length];
        Vector3[] babyLoc = new Vector3[data.children.Length];
        for (int i = 0; i < data.children.Length; i++)
        {
            babies[i].name = data.children[i].name;
            babyLoc[i].x = data.babyPos[i][0];
            babyLoc[i].y = data.babyPos[i][1];
            babyLoc[i].z = data.babyPos[i][2];
            babies[i].transform.position = babyLoc[i];            
        }
    }
}
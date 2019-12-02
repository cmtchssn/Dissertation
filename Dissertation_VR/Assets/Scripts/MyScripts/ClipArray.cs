using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClipArray : MonoBehaviour
{
    [System.Serializable]
    public struct bankNum
    {
        public AudioClip[] clip;
    }

    public bankNum[] bank = new bankNum[3];
}

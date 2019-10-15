using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipScript : MonoBehaviour
{
    public AudioClip[] clip;

    private void Start()
    {
        clip = new AudioClip[24];
    }
}

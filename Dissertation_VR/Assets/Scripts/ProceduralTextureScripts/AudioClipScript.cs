using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
[System.Serializable]
public class ClipArray
{
    public AudioClip[] clipArray;
}
*/
public class AudioClipScript : MonoBehaviour
{
    AudioSource audioSource;
    public string[][] clips =
    {
        new string[] { "metalHit", "metalHit1", "metalHit2", "metalHit3" },
        new string[] { "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04" },
        new string[] { "metalScrape", "metalScrape1", "metalScrape2", "metalScrape3" }
    };
    public AudioClip[] metalHit = new AudioClip[4];
    public AudioClip[] guitar = new AudioClip[4];
    public AudioClip[] metalScrape = new AudioClip[4];
    //public string[] clipBank = { "metalHit", "guitar", "metalScrape" };

    //public ClipArray[] clip = new ClipArray[3];

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;
        //audioSource.outputAudioMixerGroup
    }

    public void Toll(int bank, int faceVal)
    {
        audioSource.Stop();
        //audioSource.clip = Resources.Load<AudioClip>(clips[bank][faceVal]);
        if (bank == 0)
        {
            audioSource.clip = metalHit[faceVal];
        }
        else if (bank == 1)
        {
            audioSource.clip = guitar[faceVal];
        }
        else if (bank == 2)
        {
            audioSource.clip = metalScrape[faceVal];
        }
        //audioSource.clip = clip[bank][faceVal]; // Can't use [] for some reason.
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}

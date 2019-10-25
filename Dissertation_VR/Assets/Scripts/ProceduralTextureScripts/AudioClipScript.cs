using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipScript : MonoBehaviour
{
    ClipArray clipBank;
    AudioSource audioSource;
    
    /*
    public string[][] clips =
    {
        new string[] { "metalHit", "metalHit1", "metalHit2", "metalHit3" },
        new string[] { "guitarChordsD4-01", "guitarChordsD4-02", "guitarChordsD4-03", "guitarChordsD4-04" },
        new string[] { "metalScrape", "metalScrape1", "metalScrape2", "metalScrape3" }
    };

    public AudioClip[] metalHit = new AudioClip[4];
    public AudioClip[] guitar = new AudioClip[4];
    public AudioClip[] metalScrape = new AudioClip[4];
    */
    private void Start()
    {
        clipBank = GetComponent<ClipArray>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;
        //audioSource.outputAudioMixerGroup
    }

    public void Toll(int b, int faceVal)
    {
        audioSource.Stop();
        //audioSource.clip = Resources.Load<AudioClip>(clips[bank][faceVal]);
        /*
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
        */
        audioSource.clip = clipBank.bank[b].clip[faceVal];
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}

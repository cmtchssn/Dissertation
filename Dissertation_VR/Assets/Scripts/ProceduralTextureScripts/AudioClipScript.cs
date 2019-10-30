using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioClipScript : MonoBehaviour
{
    ClipArray clipBank;
    AudioSource audioSource;
    AudioClip clippy;
    public string audioDirectory; // main directory containing sub-directories of wave files.
    int subDirNum; // number of sub-directories containing wave files found in fileDirectory
    List<AudioClip> Clips = new List<AudioClip>();
    List<AudioClip>[] clippyArray;  // List array for wave files in sub-directories

    private void Start()
    {
        StartCoroutine(AudioFileFolder());
        clipBank = GetComponent<ClipArray>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;

        
        clippyArray = new List<AudioClip>[subDirNum];
        for (int i = 0; i < subDirNum; i++)
        {
            clippyArray[i] = new List<AudioClip>();
        }
        //audioSource.outputAudioMixerGroup
    }

    public void Toll(int b, int faceVal)
    {
        audioSource.Stop();
        clippy = Clips[0];
        audioSource.clip = clippy;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    IEnumerator AudioFileFolder()
    {
        var wavFiles = Directory.EnumerateFiles(audioDirectory, "*.wav"); // wave files contained in audio directory and sub-directories.
        var subDirs = Directory.EnumerateDirectories(audioDirectory, "*", SearchOption.AllDirectories); // sub-directories contained in audio directory.
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioDirectory, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Clips.Add(DownloadHandlerAudioClip.GetContent(www));
            }
        }
    }
}

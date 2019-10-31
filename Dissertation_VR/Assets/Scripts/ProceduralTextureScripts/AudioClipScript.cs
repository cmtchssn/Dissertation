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
    public string audioDirectory = @"C:\Users\localadmin\Documents\ChaseMitchusson\Dissertation\Dissertation_VR\Assets\Audio"; // main directory containing sub-directories of wave files.
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
        audioSource.clip = clippy;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    IEnumerator AudioFileFolder()
    {
        List<string> subDirs = new List<string>(Directory.EnumerateDirectories(audioDirectory));
        foreach (var dir in subDirs)
        {
            Debug.Log($"{dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
        }
        Debug.Log($"{subDirs.Count} directories found.");
        subDirNum = subDirs.Count;

        List<string> wavFiles = new List<string>(Directory.EnumerateFiles(audioDirectory, "*.wav", SearchOption.AllDirectories));
        foreach (var wav in wavFiles)
        {
            Debug.Log($"{wav.Substring(wav.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
        }
        Debug.Log($"{wavFiles.Count} wave files found.");
        Debug.Log($"{wavFiles[0]} is the first wave file.");

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

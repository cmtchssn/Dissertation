using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioClipScript : MonoBehaviour
{
    AudioSource audioSource;
    List<AudioClip>[] clippyArray;
    AudioClip clippy;
    string assetDir;
    List<string> subDirs;
    int subDirNum; // number of sub-directories containing wave files found in fileDirectory
    List<string>[] wavFiles;
    bool clipReady = false;

    private void Start()
    {
        StartCoroutine(AudioFileFolder());
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;
        //audioSource.outputAudioMixerGroup  
    }

    public void Toll(int b, int faceVal)
    {
        audioSource.Stop();
        if (clipReady)
        {
            clippy = clippyArray[b][faceVal];
            audioSource.clip = clippy;
            audioSource.Play();
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    IEnumerator AudioFileFolder()
    {
        assetDir = Application.dataPath;

        subDirs = new List<string>(Directory.EnumerateDirectories(assetDir + "/audio/"));
        subDirNum = subDirs.Count;

        wavFiles = new List<string>[subDirNum];
        for (int i = 0; i < subDirs.Count; i++)
        {
            wavFiles[i] = new List<string>(Directory.EnumerateFiles(subDirs[i] + "/", "*.wav", SearchOption.AllDirectories));
        }

        clippyArray = new List<AudioClip>[subDirNum];
        for (int i = 0; i < subDirNum; i++)
        {
            clippyArray[i] = new List<AudioClip>();
        }
        
        for (int i = 0; i < wavFiles.Length; i++)
        {
            for (int j = 0; j < wavFiles[i].Count; j++)
            {
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wavFiles[i][j], AudioType.WAV))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        clippyArray[i].Add(DownloadHandlerAudioClip.GetContent(www));
                    }
                }
            }
        }
        clipReady = true;
    }
}

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
    ProceduralD4 d4;
    ProceduralD6 d6;
    ProceduralD8 d8;
    ProceduralD10 d10;
    ProceduralD12 d12;
    ProceduralD20 d20;
    TimeSphereScript ts;
    string id;
    Object dn = null;

    private void Start()
    {
        StartCoroutine(AudioFileFolder());
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;
        /*id = gameObject.name;
        
        Debug.Log("id: " + id);
        if (id.Contains("D4"))
        {
            dn is ProceduralD4;
            d4 = GetComponent<ProceduralD4>();
        }
        else if (id.Contains("D6"))
        {
            d6 = GetComponent<ProceduralD6>();
        }
        else if (id.Contains("D8"))
        {
            d8 = GetComponent<ProceduralD8>();
        }
        else if (id.Contains("D10"))
        {
            d10 = GetComponent<ProceduralD10>();
        }
        else if (id.Contains("D10"))
        {
            d12 = GetComponent<ProceduralD12>();
        }
        else if (id.Contains("D10"))
        {
            d20 = GetComponent<ProceduralD20>();
        }*/
        
        //audioSource.outputAudioMixerGroup info here for cool effects
    }
/*
    private void OnTriggerEnter(Collider other)
    {
        if (!ts.reverse)
        {
            if (other.gameObject.layer == 15)
            {
                Toll(0, .faceVal);
            }
            if (other.gameObject.layer == 16)
            {
                Toll(0, d4.faceVal + 4);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Should the audio also play backwards?
        if (ts.reverse)
        {
            if (other.gameObject.layer == 15)
            {
                Toll(0, faceVal);
            }
            if (other.gameObject.layer == 16)
            {
                Toll(0, faceVal + 4);
            }
        }
    }
*/

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

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
    public string audioDirectory = "C:\\Users\\localadmin\\Documents\\ChaseMitchusson\\Dissertation\\Dissertation_VR\\Assets\\Audio"; // main directory containing sub-directories of wave files.
    public string aDir = @"C:/Users/localadmin/Documents/ChaseMitchusson/Dissertation/Dissertation_VR/"; // main directory containing sub-directories of wave files.
    // using a string like this won't allow others to use on other computers. I think Application.dataPath is the way to go, but I should look up how this would work on other peoples' computers.
    string m_Path;
    int subDirNum; // number of sub-directories containing wave files found in fileDirectory
    List<AudioClip> Clips = new List<AudioClip>();
    List<AudioClip>[] clippyArray;  // List array for wave files in sub-directories
    List<string>[] wavFiles;

    private void Start()
    {
        StartCoroutine(AudioFileFolder());
        clipBank = GetComponent<ClipArray>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 0.33f;
        audioSource.playOnAwake = false;
        //audioSource.outputAudioMixerGroup
        m_Path = Application.dataPath;
        Debug.Log(m_Path);
        clippyArray = new List<AudioClip>[subDirNum];
        for (int i = 0; i < subDirNum; i++)
        {
            clippyArray[i] = new List<AudioClip>();
        }
    }

    public void Toll(int b, int faceVal)
    {
        audioSource.Stop();
        clippy = clippyArray[0][0];
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
            
            Debug.Log(dir);
        }
        Debug.Log(subDirs);
        //Debug.Log($"{subDirs.Count} directories found.");
        subDirNum = subDirs.Count;
        wavFiles = new List<string>[subDirNum];
        string bigPath = m_Path + subDirs[0];
        Debug.Log(bigPath);

        for (int i = 0; i < subDirs.Count; i++)
        {
            wavFiles[i] = new List<string>(Directory.EnumerateFiles(subDirs[i], "*.wav", SearchOption.AllDirectories));
            foreach (var wav in wavFiles[i])
            {
                //Debug.Log($"{wav.Substring(wav.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
            }
            //Debug.Log($"{wavFiles[i].Count} wave files found.");
        }
        //Debug.Log(wavFiles[0][0]);

        for (int i = 0; i < wavFiles.Length; i++)
        {
            for (int j = 0; j < wavFiles[i].Count; j++)
            {
                string repair = wavFiles[i][j].ToString();
                repair = repair.Replace("\\", "/");
                wavFiles[i][j] = repair;
                //Debug.Log(aDir + wavFiles[i][j]);
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(aDir + wavFiles[i][j], AudioType.WAV))
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class timeSamplesTest : MonoBehaviour
{
    AudioSource audioSource;
    int currentSample;
    bool pause;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);
        audioSource.clip.SetData(samples, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pause = false;
            audioSource.Stop();
            //audioSource.timeSamples = Random.Range(0, 389500);
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!pause)
            {
                audioSource.Pause();
                pause = true;
            }
            else
            {
                audioSource.UnPause();
                pause = false;
            }
        }
        Debug.Log(audioSource.timeSamples);
    }
}

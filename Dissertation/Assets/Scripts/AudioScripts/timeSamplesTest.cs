using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class timeSamplesTest : MonoBehaviour
{
    AudioSource audioSource;
    bool pause;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.timeSamples = (int) Input.mousePosition.x * 190;

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

        //Debug.Log(audioSource.timeSamples);
        //Debug.Log(Input.mousePosition);
    }
}

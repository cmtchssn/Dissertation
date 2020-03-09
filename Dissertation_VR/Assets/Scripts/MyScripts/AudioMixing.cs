using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixing : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioMixerGroup[] AudioMixerGroups
    {
        get
        {
            return audioMixer.FindMatchingGroups(string.Empty);
            ;
        }
    }
}

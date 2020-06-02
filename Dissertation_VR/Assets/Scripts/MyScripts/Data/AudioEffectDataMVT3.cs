using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using HTC.UnityPlugin.Vive;

public class AudioEffectDataMVT3 : MonoBehaviour
{
    public MapGenerator mapGen;
    public AudioMixer audioMixer;

    public AudioMixerSnapshot snap1;
    public AudioMixerSnapshot snap2;
    public AudioMixerSnapshot snap3;
    public AudioMixerSnapshot snap4;
    public AudioMixerSnapshot snap5;

    public AudioMixerSnapshot[] snapArray;

    int randomSnap;
    int transitionTime;

    private void Start()
    {
        snapArray = new AudioMixerSnapshot[] { snap1, snap2, snap3, snap4, snap5 };
    }

    private void Update()
    {
        if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Grip) || ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Grip))
        {
            PickRandomSnap();
            TransitionLength();
            Debug.Log("randomSnap: " + randomSnap);
            Debug.Log("transitionLength: " + transitionTime);
            StartCoroutine(SnapTransition());
        }
    }

    IEnumerator SnapTransition()
    {
        if (mapGen.terrainLoop)
        {
            snapArray[randomSnap].TransitionTo(transitionTime);
            yield break;
        }
    }

    public int PickRandomSnap()
    {
        randomSnap = Random.Range(0, snapArray.Length - 1);
        return randomSnap;
    }

    public int TransitionLength()
    {
        transitionTime = (int)mapGen.lerpTime;
        return transitionTime;
    }
}

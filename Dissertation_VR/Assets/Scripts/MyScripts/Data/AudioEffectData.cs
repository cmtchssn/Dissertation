using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffectData : MonoBehaviour
{
    public MapGenerator mapGen;
    public AudioMixer audioMixer;

    public float echoDelay;
    public float echoDecay;
    public float echoMaxCh;
    public float echoDry;
    public float echoWet;
    public float echoVol;

    public float chorDry;
    public float chorWet1;
    public float chorWet2;
    public float chorWet3;
    public float chorDelay;
    public float chorRate;
    public float chorDepth;
    public float chorFeedback;
    public float chorVol;

    void Update()
    {
        echoDelay = ScaleVals(mapGen.noiseData.noiseScale, 5, 10, 1, 5000);
        echoDecay = ScaleVals(mapGen.terrainData.meshHeightMultiplier, -1.7f, 1.7f, 0, 1);
        echoMaxCh = ScaleVals(mapGen.noiseData.lacunarity, 1, 3, 1, 2);
        echoDry = ScaleVals(mapGen.noiseData.offset.x, -10, 10, 0, 1);
        echoWet = ScaleVals(mapGen.noiseData.offset.y, -10, 10, 0, 1);
        echoVol = ScaleVals(mapGen.noiseData.persistance, 0.15f, 0.65f, -3, 3);

        SetEchoData(echoDelay, echoDecay, echoMaxCh, echoDry, echoWet, echoVol);

        chorDry = ScaleVals(mapGen.noiseData.offset.y, -10, 10, 0, 1);
        chorWet1 = ScaleVals(mapGen.terrainData.meshHeightMultiplier, -1.7f, 1.7f, 0, 1);
        chorWet2 = ScaleVals(mapGen.noiseData.noiseScale, 5, 10, 0, 1);
        chorWet3 = ScaleVals(mapGen.noiseData.lacunarity, 1, 3, 0, 1);
        chorDelay = ScaleVals(mapGen.noiseData.persistance, 0.15f, 0.65f, 0, 100);
        chorRate = ScaleVals(mapGen.noiseData.noiseScale, 5, 10, 0, 20);
        chorDepth = ScaleVals(mapGen.noiseData.offset.x, -10, 10, 0, 1);
        chorFeedback = ScaleVals(mapGen.noiseData.lacunarity, 1, 3, -1, 1);
        chorVol = ScaleVals(mapGen.noiseData.offset.x, -10, 10, -3, 3);

        SetChorusData(chorDry, chorWet1, chorWet2, chorWet3, chorDelay, chorRate, chorDepth, chorFeedback, chorVol);
    }

    public float ScaleVals(float currentVal, float oldMin, float oldMax, float newMin, float newMax)
    {
        float scaledVals = (newMax - newMin) * (currentVal - oldMin) / (oldMax - oldMin) + newMin;

        return scaledVals;
    }

    public void SetEchoData(float delay, float decay, float maxCh, float dry, float wet, float vol)
    {
        audioMixer.SetFloat("echoDelay", delay);
        audioMixer.SetFloat("echoDecay", decay);
        audioMixer.SetFloat("echoMaxCh", maxCh);
        audioMixer.SetFloat("echoDry", dry);
        audioMixer.SetFloat("echoWet", wet);
        audioMixer.SetFloat("echoVol", vol);
    }

    public void SetChorusData(float dry, float wet1, float wet2, float wet3, float delay, float rate, float depth, float fb, float vol)
    {
        audioMixer.SetFloat("chorDry", dry);
        audioMixer.SetFloat("chorWet1", wet1);
        audioMixer.SetFloat("chorWet2", wet2);
        audioMixer.SetFloat("chorWet3", wet3);
        audioMixer.SetFloat("chorDelay", delay);
        audioMixer.SetFloat("chorRate", rate);
        audioMixer.SetFloat("chorDepth", depth);
        audioMixer.SetFloat("chorFeedback", fb);
        audioMixer.SetFloat("chorVol", vol);
    }


}

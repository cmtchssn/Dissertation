using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class timeMenuValues : ScriptableObject
{
    public int layerVal = 0;
    public float minSizeVal = 0.2f;
    public float maxSizeVal = 120f;
    public float speedVal = 1f;
    public float durationVal = 5f;
    public bool reverseVal = false;

    public void SetLayerVal(float v)
    {
        layerVal = (int)v;
    }

    public void SetMinSizeVal(float v)
    {
        minSizeVal = v;
    }

    public void SetMaxSizeVal(float v)
    {
        maxSizeVal = v;
    }

    public void SetSpeedVal(float v)
    {
        speedVal = v;
    }

    public void SetDurationVal(float v)
    {
        durationVal = v;
    }

    public void SetReverseVal(bool v)
    {
        reverseVal = v;
    }
}

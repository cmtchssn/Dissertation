﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSphereScript : MonoBehaviour
{
    public GameObject spaceSphere;
    public Vector3 minScale = new Vector3(1, 1, 1);
    public Vector3 maxScale;
    public bool repeatable;
    public bool reverse;
    public float speed = 1f;
    public float duration = 5f;
    public Vector3 minOffset = new Vector3(0, 0, 0);
    public Vector3 maxOffset = new Vector3(0, 0, 0);

    IEnumerator Start ()
    {
        //minScale = transform.localScale;
        while (repeatable)
        {
            if (reverse)
            {
                yield return RepeatLerp(maxScale, minScale, duration);
            }
            else
            {
                yield return RepeatLerp(minScale, maxScale, duration);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = spaceSphere.transform.position;
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            if (reverse)
            {
                transform.localScale = Vector3.Lerp(a - maxOffset, b + minOffset, i);
            }
            else
            {
                transform.localScale = Vector3.Lerp(a + minOffset, b - maxOffset, i);
            }
            yield return null;
        }
    }

}

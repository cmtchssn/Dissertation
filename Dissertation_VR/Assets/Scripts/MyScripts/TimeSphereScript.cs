using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSphereScript : MonoBehaviour
{
    public GameObject spaceSphere;
    public GameObject timeAndSpace;
    SpaceSphereScript sp;
    int[] layerPool = new int[] { 15, 16 };
    public int layer = 0;
    public Vector3 minScale;
    public Vector3 maxScale;
    public float minSlide = 0.2f;
    public float maxSlide = 120f;
    public bool repeatable;
    public bool reverse;
    public float speed = 1f;
    public float duration = 5f;
    public timeMenuValues tmv;
    
    IEnumerator Start ()
    {
        sp = spaceSphere.GetComponent<SpaceSphereScript>();
        
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
        if (sp.sphereHold && sp.thisTime)
        {
            SetTimeValues();
            //Debug.Log("SetTimeValues has just Run.");
        }
        transform.position = spaceSphere.transform.position;
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        //Debug.Log("minSlide: " + minSlide + " and maxSlide: " + maxSlide);
        maxScale = new Vector3(maxSlide, maxSlide, maxSlide);
        minScale = new Vector3(minSlide, minSlide, minSlide);
        gameObject.layer = layerPool[layer];
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public void SetTimeValues()
    {
        layer = tmv.layerVal;
        minSlide = tmv.minSizeVal;
        maxSlide = tmv.maxSizeVal;
        speed = tmv.speedVal;
        duration = tmv.durationVal;
        reverse = tmv.reverseVal;
    }
    
    public void Del()
    {
        sp.sphereHold = false;
        Destroy(timeAndSpace);
    }
}

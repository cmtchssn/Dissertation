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
    //public Vector3 minOffset = new Vector3(0, 0, 0);
    //public Vector3 maxOffset = new Vector3(0, 0, 0);
    
    IEnumerator Start ()
    {
        sp = spaceSphere.GetComponent<SpaceSphereScript>();
        maxScale = new Vector3(maxSlide, maxSlide, maxSlide);
        minScale = new Vector3(minSlide, minSlide, minSlide);
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

    public void SetLayer(float s)
    {
        layer = (int) s;
    }

    public void SetMin(float s)
    {
        minSlide = s;
    }

    public void SetMax(float s)
    {
        maxSlide = s;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetRev(bool t)
    {
        reverse = t;
    }

    public void Del()
    {
        sp.sphereHold = false;
        Destroy(timeAndSpace);
    }
}

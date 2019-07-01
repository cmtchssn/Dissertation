using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class vocodeParams : MonoBehaviour
{
    public AudioMixer vocGroup;
    public GameObject soundObject;

    private float xPos;
    private float yPos;
    private float zPos;
    private float xRot;
    private float yRot;
    private float zRot;

    // Start is called before the first frame update
    void Start()
    {
        vocGroup = GetComponent<AudioMixer>();
        soundObject = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        xPos = soundObject.transform.position.x;
        yPos = soundObject.transform.position.y * 0.05f;
        zPos = soundObject.transform.position.z * 0.1f;
        xRot = soundObject.transform.rotation.x * 0.1f;
        yRot = soundObject.transform.rotation.y * 0.01f;
        zRot = soundObject.transform.rotation.z;

        if (xPos <= -1500f)
        {
            xPos = -1500f;
        }
        if (xPos >= 1500f)
        {
            xPos = 1500f;
        }

        if (yPos <= 0.05f)
        {
            yPos = 0.05f;
        }
        if (yPos >= 10f)
        {
            yPos = 10f;
        }

        if (zPos <= 0.1f)
        {
            zPos = 0.1f;
        }
        if (zPos >= 100f)
        {
            zPos = 100f;
        }

        if (xRot <= 0.1f)
        {
            xRot = 0.1f;
        }
        if (xRot >= 100f)
        {
            xRot = 100f;
        }

        if (yRot <= 0f)
        {
            yRot = 0f;
        }
        if (yRot >= 0.4f)
        {
            yRot = 0.4f;
        }

        if (zRot <= 50f)
        {
            zRot = 50f;
        }
        if (zRot >= 150f)
        {
            zRot = 150f;
        }

        vocGroup.SetFloat("VocodeFormantShift", xPos);
        vocGroup.SetFloat("VocodeFormantScale", yPos);
        vocGroup.SetFloat("VocodeAnalysisBW", zPos);
        vocGroup.SetFloat("VocodeSynthesisBW", xRot);
        vocGroup.SetFloat("VocodeEnvelopeDecay", yRot);
        vocGroup.SetFloat("VocodeEmphasis", zRot);

    }
}

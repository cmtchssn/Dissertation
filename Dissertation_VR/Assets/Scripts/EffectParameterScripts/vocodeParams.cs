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
        // I apparently don't need to get any components, as they are not attached to the game object.
    }

    // Update is called once per frame
    void Update()
    {
        //position is in units (infinite), but x is currently -50. to 50., y is 0 to inf, z is -50. to 50.
        xPos = soundObject.transform.position.x * 30f; // scale to -1500. to 1500.
        yPos = (soundObject.transform.position.y - 0.5f) * 1.111f; // scale to 0. to 10.
        zPos = (soundObject.transform.position.z * 0.01f) + 0.5f; // scale to 0. to 1.

        //rotation is between -1. and 1.
        xRot = (soundObject.transform.rotation.x * 0.5f) + 0.5f;
        yRot = (soundObject.transform.rotation.y * 0.2f) + 0.2f;
        zRot = (soundObject.transform.rotation.z * 0.5f) + 1f;
        /*
        if (xPos <= -1500f)
        {
            xPos = -1500f;
        }
        if (xPos >= 1500f)
        {
            xPos = 1500f;
        }
        */
        if (yPos <= 0.05f)
        {
            yPos = 0.05f;
        }
        /*
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
        */
        vocGroup.SetFloat("VocodeFormantShift", xPos);
        vocGroup.SetFloat("VocodeFormantScale", yPos);
        vocGroup.SetFloat("VocodeAnalysisBW", zPos);
        vocGroup.SetFloat("VocodeSynthesisBW", xRot);
        vocGroup.SetFloat("VocodeEnvelopeDecay", yRot);
        vocGroup.SetFloat("VocodeEmphasis", zRot);
        vocGroup.SetFloat("VolumeVoc", 0f);

        //Debug.Log("xpos: " + xPos + "; ypos: " + yPos + "; zpos: " + zPos);
        //Debug.Log("xrot: " + xRot + "; yrot: " + yRot + "; zrot: " + zRot);
    }
}

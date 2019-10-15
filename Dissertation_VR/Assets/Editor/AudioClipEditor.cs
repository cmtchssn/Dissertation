using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioClipScript))]
public class AudioClipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //AudioClipScript clipScript = (AudioClipScript)target;
        //clipScript.clipnum = EditorGUILayout.IntField("Clip #", clipScript.clipnum);
    }
}

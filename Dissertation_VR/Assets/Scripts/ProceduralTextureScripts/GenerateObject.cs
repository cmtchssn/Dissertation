using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    GameObject emptyObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            emptyObj = new GameObject("D4");
            emptyObj.transform.position = this.gameObject.transform.position;
            emptyObj.AddComponent<ProceduralD4>();
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
        }
    }
}

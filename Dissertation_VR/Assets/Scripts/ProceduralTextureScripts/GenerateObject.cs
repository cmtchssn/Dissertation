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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            generateD4();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            generateD8();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            generateD6();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            generateD10();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            generateD12();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            generateD20();
        }
    }

    void generateD4()
    {
        emptyObj = new GameObject("D4");
        emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
        emptyObj.AddComponent<ProceduralD4>();
        emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
    }

    void generateD6()
    {
        emptyObj = new GameObject("D6");
        emptyObj.transform.position = this.gameObject.transform.position;
        emptyObj.AddComponent<ProceduralD6>();
        emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
    }

    void generateD8()
    {
        emptyObj = new GameObject("D8");
        emptyObj.transform.position = this.gameObject.transform.position;
        emptyObj.AddComponent<ProceduralD8>();
        emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
    }

    void generateD10()
    {
        emptyObj = new GameObject("D10");
        emptyObj.transform.position = this.gameObject.transform.position;
        emptyObj.AddComponent<ProceduralD10>();
        emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
    }

    void generateD12()
    {
        emptyObj = new GameObject("D12");
        emptyObj.transform.position = this.gameObject.transform.position;
        emptyObj.AddComponent<ProceduralD12>();
        emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
    }

    void generateD20()
    {
        emptyObj = new GameObject("D20");
        emptyObj.transform.position = this.gameObject.transform.position;
        emptyObj.AddComponent<ProceduralD20>();
        emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
    }
}

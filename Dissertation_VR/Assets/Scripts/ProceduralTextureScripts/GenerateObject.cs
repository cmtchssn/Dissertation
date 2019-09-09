using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    GameObject emptyObj;
    int count4 = 0;
    int count6 = 0;
    int count8 = 0;
    int count10 = 0;
    int count12 = 0;
    int count20 = 0;

    // Update is called once per frame
    void Update()
    {
        generateDn();
    }

    void generateDn()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string name = "D4-" + count4;
            emptyObj = new GameObject(name); // consolidate these functions as one that can generate whatever based on key pressed
            emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
            emptyObj.AddComponent<ProceduralD4>(); // this should change based on input
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
            count4++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string name = "D6-" + count6;
            emptyObj = new GameObject(name); // consolidate these functions as one that can generate whatever based on key pressed
            emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
            emptyObj.AddComponent<ProceduralD6>(); // this should change based on input
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
            count6++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            string name = "D8-" + count8;
            emptyObj = new GameObject(name); // consolidate these functions as one that can generate whatever based on key pressed
            emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
            emptyObj.AddComponent<ProceduralD8>(); // this should change based on input
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
            count8++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            string name = "D10-" + count10;
            emptyObj = new GameObject(name); // consolidate these functions as one that can generate whatever based on key pressed
            emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
            emptyObj.AddComponent<ProceduralD10>(); // this should change based on input
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
            count10++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            string name = "D12-" + count12;
            emptyObj = new GameObject(name); // consolidate these functions as one that can generate whatever based on key pressed
            emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
            emptyObj.AddComponent<ProceduralD12>(); // this should change based on input
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
            count12++;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            string name = "D20-" + count20;
            emptyObj = new GameObject(name); // consolidate these functions as one that can generate whatever based on key pressed
            emptyObj.transform.position = this.gameObject.transform.position; // learn how to adjust the position to right in front of the player
            emptyObj.AddComponent<ProceduralD20>(); // this should change based on input
            emptyObj.AddComponent<HTC.UnityPlugin.Vive.BasicGrabbable>();
            count20++;
        }
    }
}
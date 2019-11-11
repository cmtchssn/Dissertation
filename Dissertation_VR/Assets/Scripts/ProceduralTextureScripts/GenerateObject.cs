using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class GenerateObject : MonoBehaviour
{
    GameObject emptyObj;
    public GameObject shapeMenu;
    public GameObject D4;
    int count4 = 0;
    int count6 = 0;
    int count8 = 0;
    int count10 = 0;
    int count12 = 0;
    int count20 = 0;
    bool menuExists = false;
    public Transform mama;

    // a Scene where you use vive controllers to press against a force would be cool

    void Awake()
    {
        shapeMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        generateDn();
        VivePress();
    }

    void VivePress()
    {
        // Click menu button
        if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Menu) || ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Menu))
        {   
            if (!menuExists)
            {
                // A scrollable menu pops up with numbers and icons of the 6 spawnable objects
                shapeMenu.gameObject.SetActive(true);
                menuExists = true;
                Debug.Log("If: " + menuExists);
                // Scroll up and down trackpad to highlight object you want to spawn
                // click thumb pad to select highlighted object you want to spawn
                // spawn that object as being held by the controller used to select and spawn object
                // use trigger to throw/drop object from controller
            }
            else
            {
                shapeMenu.gameObject.SetActive(false);
                menuExists = false;
                Debug.Log("Else: " + menuExists);
            }
        }
    }

    public void generateD4()
    {
        D4.name = "D4-" + count4;
        GameObject myD4 = Instantiate(D4, new Vector3(0, 2, 0), Quaternion.identity);
        myD4.transform.parent = mama;
        count4++;
    }

    public void generateDn()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            D4.name = "D4-" + count4;
            GameObject myD4 = Instantiate(D4, new Vector3(0, 2, 0), Quaternion.identity);
            myD4.transform.parent = mama;
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
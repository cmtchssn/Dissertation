using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class GenerateObject : MonoBehaviour
{
    GameObject emptyObj;
    public GameObject shapeMenu;
    public GameObject D4;
    public GameObject D6;
    public GameObject D8;
    public GameObject D10;
    public GameObject D12;
    public GameObject D20;
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

    public void generateD6()
    {
        D6.name = "D6-" + count6;
        GameObject myD6 = Instantiate(D6, new Vector3(0, 2, 0), Quaternion.identity);
        myD6.transform.parent = mama;
        count6++;
    }

    public void generateD8()
    {
        D8.name = "D8-" + count8;
        GameObject myD8 = Instantiate(D8, new Vector3(0, 2, 0), Quaternion.identity);
        myD8.transform.parent = mama;
        count8++;
    }

    public void generateD10()
    {
        D10.name = "D10-" + count10;
        GameObject myD10 = Instantiate(D10, new Vector3(0, 2, 0), Quaternion.identity);
        myD10.transform.parent = mama;
        count10++;
    }

    public void generateD12()
    {
        D12.name = "D12-" + count12;
        GameObject myD12 = Instantiate(D12, new Vector3(0, 2, 0), Quaternion.identity);
        myD12.transform.parent = mama;
        count12++;
    }

    public void generateD20()
    {
        D20.name = "D20-" + count20;
        GameObject myD20 = Instantiate(D20, new Vector3(0, 2, 0), Quaternion.identity);
        myD20.transform.parent = mama;
        count20++;
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
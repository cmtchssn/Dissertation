using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class SpaceSphereScript : MonoBehaviour
{
    public bool sphereHold = false;
    public GameObject right;
    public GameObject left;
    bool timeUI = false;
    public GameObject shapeMenu;
    public GameObject timeMenu;
    //bool shapeUI = false;
    public Canvas canvas;
    //public GenerateObject genObj;
    public Camera cam;

    private void Update()
    {
        VivePress();
    }

    private void LateUpdate()
    {
        //canvas.transform.parent = cam.transform;
        canvas.transform.localPosition = Vector3.forward * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == (right||left))
        {
            sphereHold = true;
            Debug.Log("sphereHold on enter is: " + sphereHold);
        }
        else
        {
            sphereHold = false;
            Debug.Log("sphereHold on enter w/o controller is: " + sphereHold);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == (right||left))
        {
            sphereHold = false;
            Debug.Log("sphereHold on exit is: " + sphereHold);
        }
    }

    void VivePress()
    {
        if (sphereHold)
        {
            if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Menu) || ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Menu))
            {
                if (!timeUI)
                {
                    //open timesphere menu
                    shapeMenu.gameObject.SetActive(false);
                    timeMenu.gameObject.SetActive(true);
                    timeUI = true;
                }
                else
                {
                    //open timesphere menu
                    shapeMenu.gameObject.SetActive(false);
                    timeMenu.gameObject.SetActive(false);
                    timeUI = false;
                }
            }
        }
    }
}

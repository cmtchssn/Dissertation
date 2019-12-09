using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSphereScript : MonoBehaviour
{
    public bool sphereHold = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name == "Right" || other.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name == "Left")
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
        if (other.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name == "Right" || other.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name == "Left")
        {
            sphereHold = false;
            Debug.Log("sphereHold on exit is: " + sphereHold);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class certainDeath : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "TimeSphere")
        {
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayTest : MonoBehaviour
{
    void Update()
    {
        Ray ray1 = new Ray(transform.position, transform.forward);
        Ray ray2 = new Ray(transform.position, -transform.forward);
        Ray ray3 = new Ray(transform.position, transform.up);
        Ray ray4 = new Ray(transform.position, -transform.up);
        Ray ray5 = new Ray(transform.position, transform.right);
        Ray ray6 = new Ray(transform.position, -transform.right);
        //RaycastHit hitInfo;

        Debug.DrawLine(ray1.origin, ray1.origin + ray1.direction * 1, Color.green);
        Debug.DrawLine(ray2.origin, ray2.origin + ray2.direction * 1, Color.green);
        Debug.DrawLine(ray3.origin, ray3.origin + ray3.direction * 1, Color.green);
        Debug.DrawLine(ray4.origin, ray4.origin + ray4.direction * 1, Color.green);
        Debug.DrawLine(ray5.origin, ray5.origin + ray5.direction * 1, Color.green);
        Debug.DrawLine(ray6.origin, ray6.origin + ray6.direction * 1, Color.green);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFall : MonoBehaviour
{
    public LayerMask mask;

    void Update()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), -Vector3.up);
        RaycastHit hitInfo;

        if (Physics.Raycast (ray, out hitInfo, 100, mask))
        {
            Debug.DrawRay(ray.origin, hitInfo.point, Color.red);
            transform.position = hitInfo.point;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFall : MonoBehaviour
{
    public LayerMask mask;

    private void Start()
    {
        mask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        Ray rayD = new Ray(new Vector3(transform.position.x, transform.position.y + 100, transform.position.z), -Vector3.up * 100);
        //Ray rayU = new Ray(new Vector3(transform.position.x, transform.position.y + 100, transform.position.z), -Vector3.up * 100);
        Debug.DrawRay(rayD.origin, -Vector3.up, Color.magenta);
        //Debug.DrawRay(rayU.origin, -Vector3.up, Color.cyan);
        //Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hitInfo;

        if (Physics.Raycast (rayD, out hitInfo, 1000, mask))
        {
            Debug.DrawRay(rayD.origin, -Vector3.up, Color.yellow);
            //Debug.DrawRay(transform.position, transform.TransformVector(-transform.up), Color.magenta);
            transform.position = hitInfo.point;
        }
        //else if (Physics.Raycast(rayU, out hitInfo, 1001, mask))
        //{
            //Debug.DrawRay(rayU.origin, -Vector3.up, Color.gray);
            //Debug.DrawRay(transform.position, transform.TransformVector(-transform.up), Color.magenta);
            //transform.position = hitInfo.point;
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charControl : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * speed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.up * speed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.down * speed);
        }
    }
}

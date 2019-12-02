using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class certainDeath : MonoBehaviour
{
    public GenerateObject gob;


    private void Start()
    {
        gob = GetComponent<GenerateObject>();
        gob.mom.kids = new Dictionary<string, GameObject>();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Destroy: " + gob.mom.kids[other.name]);
        gob.mom.kids.Remove(other.name);
        Destroy(other.gameObject);
    }
}

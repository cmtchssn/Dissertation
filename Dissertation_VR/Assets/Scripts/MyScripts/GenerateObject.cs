using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.UI;

public class GenerateObject : MonoBehaviour
{
    GameObject emptyObj;
    public GameObject playerCam;
    public GameObject shapeMenu;
    public GameObject timeMenu;
    public GameObject timeSpace;
    public GameObject spaceSphere;
    public GameObject timeSphere;
    public SpaceSphereScript sp;
    public GameObject D4;
    public GameObject D6;
    public GameObject D8;
    public GameObject D10;
    public GameObject D12;
    public GameObject D20;
    public GameObject rhs;
    public GameObject lhs;
    public float dist = 2f;
    Vector3 playerFront;
    int countT = 0;
    int count4 = 0;
    int count6 = 0;
    int count8 = 0;
    int count10 = 0;
    int count12 = 0;
    int count20 = 0;
    bool menuExists = false;
    public Transform mama;
    public DnParent mom;
    bool timeUI = false;
    
    //public Dictionary<string, GameObject> kids;
    // a Scene where you use vive controllers to press against a force would be cool

    void Awake()
    {
        shapeMenu.gameObject.SetActive(false);
        mom = new DnParent();
        mom.kids = new Dictionary<string, GameObject>();
        //kids = new Dictionary<string, GameObject>();
        playerFront = (playerCam.transform.forward * dist) + playerCam.transform.position;
        sp = spaceSphere.GetComponent<SpaceSphereScript>();
    }

    // Update is called once per frame
    void Update()
    {
        VivePress();
    }
    
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Destroy: " + mom.kids[other.gameObject.name]);
        //mom.kids.Remove(other.gameObject.name);
        Destroy(other.gameObject);
    }

    void VivePress()
    {
        // Click menu button
        if (!sp.sphereHold)
        {
            if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Menu) || ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Menu))
            {
                if (!menuExists)
                {
                    if (timeUI)
                    {
                        shapeMenu.gameObject.SetActive(false);
                        menuExists = false;
                    }
                    else
                    {
                        shapeMenu.gameObject.SetActive(true);
                        sp.timeMenu.gameObject.SetActive(false);
                        menuExists = true;
                    }
                }
                else
                {
                    shapeMenu.gameObject.SetActive(false);
                    menuExists = false;
                    sp.timeMenu.gameObject.SetActive(false);
                    timeUI = false;
                }
            }
        }
        
    }

    public void generateTime()
    {
        timeSpace.name = "TimeSpace-" + countT;
        GameObject myTS = Instantiate(timeSpace, playerFront, Quaternion.identity);
        myTS.transform.parent = mama;
        countT++;
    }

    public void generateD4()
    {
        D4.name = "D4-" + count4;
        GameObject myD4 = Instantiate(D4, playerFront, Quaternion.identity);
        myD4.transform.parent = mama;
        //Vector3 cont = ViveInput.Instance.transform.position;
        //mom.kids.Add(D4.name + "(Clone)", D4);
        //Debug.Log("Generate: " + mom.kids[D4.name + "(Clone)"]);
        count4++;
    }

    public void generateD6()
    {
        D6.name = "D6-" + count6;
        GameObject myD6 = Instantiate(D6, new Vector3(lhs.transform.position.x, lhs.transform.position.y + 2, lhs.transform.position.z), Quaternion.identity);
        myD6.transform.parent = mama;
        mom.kids.Add(D6.name, D6);
        count6++;
    }

    public void generateD8()
    {
        D8.name = "D8-" + count8;
        GameObject myD8 = Instantiate(D8, new Vector3(rhs.transform.position.x, rhs.transform.position.y + 2, rhs.transform.position.z), Quaternion.identity);
        myD8.transform.parent = mama;
        mom.kids.Add(D8.name, D8);
        count8++;
    }

    public void generateD10()
    {
        D10.name = "D10-" + count10;
        GameObject myD10 = Instantiate(D10, new Vector3(lhs.transform.position.x, lhs.transform.position.y + 2, lhs.transform.position.z), Quaternion.identity);
        myD10.transform.parent = mama;
        mom.kids.Add(D10.name, D10);
        count10++;
    }

    public void generateD12()
    {
        D12.name = "D12-" + count12;
        GameObject myD12 = Instantiate(D12, new Vector3(lhs.transform.position.x, lhs.transform.position.y + 2, lhs.transform.position.z), Quaternion.identity);
        myD12.transform.parent = mama;
        mom.kids.Add(D12.name, D12);
        count12++;
    }

    public void generateD20()
    {
        D20.name = "D20-" + count20;
        GameObject myD20 = Instantiate(D20, new Vector3(lhs.transform.position.x, lhs.transform.position.y + 2, lhs.transform.position.z), Quaternion.identity);
        myD20.transform.parent = mama;
        mom.kids.Add(D20.name, D20);
        count20++;
    }

    public void editTimeSphere()
    {

    }
}
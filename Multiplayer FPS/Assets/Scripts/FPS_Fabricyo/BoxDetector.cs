using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    public GameObject boxHolder;

    public GameObject pickTxt;

    public GameObject dropTxt;

    public GameObject highLightBox;

    public bool canCollect;

    public bool isHolding;

    GameObject collectablesGroup;

    GameObject rescueZone;

    private void Start()
    {
        collectablesGroup = GameObject.Find("COLLECTABLES");
        rescueZone = GameObject.FindGameObjectWithTag("Rescue Zone");
    }

    private void Update()
    {
        if (canCollect && !isHolding)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpBox();
            }
        }

        if (isHolding)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                DropBox();
            }
        }
    }

    public void PickUpBox()
    {
        //highLightBox.GetComponent<BoxCollider>().enabled = false;

        GetComponent<Movement>().speedStage = 0;

        highLightBox.transform.SetParent(boxHolder.transform);

        highLightBox.transform.position = boxHolder.transform.position + new Vector3(0, 0.15f, 0);

        highLightBox.transform.localRotation = Quaternion.Euler(0, 0, 0);

        boxHolder.GetComponent<Animator>().SetTrigger("PickUp");

        highLightBox.GetComponent<Rigidbody>().isKinematic = true;

        isHolding = true;

        pickTxt.gameObject.SetActive(false);

        dropTxt.gameObject.SetActive(true);

        
    }

    public void DropBox()
    {
        //highLightBox.GetComponent<BoxCollider>().enabled = true;

        GetComponent<Movement>().speedStage = 1;

        dropTxt.gameObject.SetActive(false);

        highLightBox.transform.parent = collectablesGroup.transform;

        highLightBox.GetComponent<Rigidbody>().isKinematic = false;
        
        highLightBox.transform.localRotation = Quaternion.Euler(0, highLightBox.transform.localRotation.y, highLightBox.transform.localRotation.z);

        highLightBox = null;

        isHolding = false;

        canCollect = true;

        rescueZone.GetComponent<RescueZone>().TriggerList.Add(highLightBox.GetComponent<BoxCollider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            if (!isHolding)
            {
                canCollect = true;
            }

            if (!pickTxt.gameObject.activeSelf && canCollect)
            {
                pickTxt.SetActive(true);

                highLightBox = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            canCollect = false;

            if (pickTxt.gameObject.activeSelf)
            {
                pickTxt.SetActive(false);
            }

            if (!isHolding)
            {
                highLightBox = null;
            }
        }
    }
}

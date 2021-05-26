using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemFP : MonoBehaviour
{
    
    GameObject collectablesGroup;
    public GameObject box;

    private void Start()
    {
        collectablesGroup = GameObject.Find("COLLECTABLES");
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            DropBox();
        }
    }

    public void DropBox()
    {
        GameObject newBox = Instantiate(box, transform.position + transform.forward.normalized * 0.23f + transform.up.normalized * 0.23f, Quaternion.identity);
        newBox.transform.parent = collectablesGroup.transform;
    }
}

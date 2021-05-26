using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropItemPC : MonoBehaviour
{
    GameObject socket;
    GameObject collectablesGroup;
    Canvas playerGUI;
    GameObject getCanvas;
    Animator playerAnim;
    NavMeshAgent agent;
    public GameObject box;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        socket = transform.Find("mixamorig:LeftHand").gameObject.transform.Find("Socket").gameObject;
        getCanvas = transform.Find("PlayerGUI").gameObject;
        playerGUI = getCanvas.GetComponent<Canvas>();
        collectablesGroup = GameObject.Find("COLLECTABLES");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (playerAnim.GetBool("CarryingBox") && Input.GetMouseButtonDown(1))
        {
            AnimNotBox();
            agent.speed = 1;

            if (!Input.GetKey(KeyCode.LeftControl))
            {
                DropBox();
            }
        }

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
    public void AnimNotBox()
    {
        socket.SetActive(false);
        playerAnim.SetBool("CarryingBox", false);
        playerGUI.enabled = false;
    }
}

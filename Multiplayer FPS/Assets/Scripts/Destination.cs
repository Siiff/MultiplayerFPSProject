using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class Destination : MonoBehaviour
{
    public EventEnvironment OnClickEnvironment;
    public GameObject player;

    public GameObject doorWayDestination;
    public GameObject doorWayBackDestination;
    public Vector3 destination;
    public Vector3 destinationBack;

    public Texture2D cursorDoorWay;
    public Texture2D cursorDoorBack;

    public Canvas gateEnterGUI;
    public Canvas gateExitGUI;

    private Animator gateAnim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        destination = doorWayDestination.GetComponent<Transform>().position;
        destinationBack = doorWayBackDestination.GetComponent<Transform>().position;
        gateAnim = GetComponent<Animator>();
        gateEnterGUI = transform.Find("GateEnterGUI").GetComponent<Canvas>();
        gateExitGUI = transform.Find("GateExitGUI").GetComponent<Canvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gateAnim.SetBool("open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gateAnim.SetBool("open", false);
        }
    }

    private void OnMouseOver()
    {
        if (this.CompareTag("DoorWay"))
        {
            Cursor.SetCursor(cursorDoorWay, new  Vector2(0,0), CursorMode.ForceSoftware);
            gateEnterGUI.enabled = false;
            gateExitGUI.enabled = true;
        }
        else if (this.CompareTag("DoorWayBack"))
        {
            Cursor.SetCursor(cursorDoorBack, new Vector2(0, 0), CursorMode.ForceSoftware);
            gateExitGUI.enabled = false;
            gateEnterGUI.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        gateExitGUI.enabled = false;
        gateEnterGUI.enabled = false;
    }

    private void OnMouseDown()
    {
        EventEnvironmentArgs args;
        switch (this.tag)
        {
            case "DoorWay":
                args = new EventEnvironmentArgs();
                args.destination = destination;
                args.tag = this.tag;
                player.GetComponent<Player>().Move(args);

                this.tag = "DoorWayBack";
                break;

            case "DoorWayBack":
                args = new EventEnvironmentArgs();
                args.destination = destinationBack;
                args.tag = this.tag;
                player.GetComponent<Player>().Move(args);

                this.tag = "DoorWay";
                break;
        }
    }
}
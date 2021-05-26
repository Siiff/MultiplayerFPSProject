using UnityEngine;

public class Box : MonoBehaviour
{
    GameObject player;
    GameObject socket;
    GameObject getCanvas;
    GameObject rescueZone;
    Canvas playerGUI;
    Canvas boxGUI;
    Animator animPlayer;
    bool inRescueZone = false;

    private void Start()
    {
        // Celso*: get all gameobjects and Canvas needed automatically 
        player = GameObject.FindWithTag("Player");
        animPlayer = player.GetComponent<Animator>();
        socket = player.transform.Find("mixamorig:LeftHand").gameObject.transform.Find("Socket").gameObject;
        getCanvas = player.transform.Find("PlayerGUI").gameObject;
        playerGUI = getCanvas.GetComponent<Canvas>();
        boxGUI = transform.Find("BoxGUI").GetComponent<Canvas>();
        rescueZone = GameObject.FindWithTag("Rescue Zone");
    }

    private void OnTriggerStay(Collider other)
    {
        if(!animPlayer.GetBool("CarryingBox") && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            socket.SetActive(true);
            playerGUI.enabled = true;
            animPlayer.SetBool("CarryingBox", true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boxGUI.enabled = true;
        }

        if (other.CompareTag("Rescue Zone"))
        {
            inRescueZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boxGUI.enabled = false;
        }

        if (other.CompareTag("Rescue Zone"))
        {
            rescueZone.GetComponent<RescueZone>().SubtractCounter();
        }
    }

    private void OnDestroy()
    {
        if (inRescueZone)
        {
            rescueZone.GetComponent<RescueZone>().SubtractCounter();
        }
    }
}

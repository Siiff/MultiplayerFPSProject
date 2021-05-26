using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BoxPC : MonoBehaviour
{
    GameObject player;
    GameObject socket;
    GameObject getCanvas;
    GameObject rescueZone;
    Canvas playerGUI;
    Canvas boxGUI;
    Animator animPlayer;
    bool inRescueZone = false;

    public bool playerWannaGet = false;

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

    private void OnMouseDown()
    {
        playerWannaGet = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!animPlayer.GetBool("CarryingBox") && other.CompareTag("Player") && playerWannaGet)
        {
            socket.SetActive(true);
            playerGUI.enabled = true;
            animPlayer.SetBool("CarryingBox", true);
            player.GetComponent<NavMeshAgent>().speed = 0.25f;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Rescue Zone"))
        {
            inRescueZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

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

    public void OnMouseOver()
    {
        boxGUI.enabled = true;
    }

    private void OnMouseExit()
    {
        boxGUI.enabled = false;
    }
}

[System.Serializable]
public class EventGetBox : UnityEvent<EventGetBoxArgs>
{

}

public class EventGetBoxArgs
{
    public Vector3 destination;
    public string tag;
}
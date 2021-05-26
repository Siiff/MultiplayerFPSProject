using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBox : MonoBehaviour
{
    GameObject player;
    GameObject collectablesGroup;
    public GameObject box;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        collectablesGroup = GameObject.Find("COLLECTABLES");
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        GameObject newBox = Instantiate(box, player.transform.position + player.transform.forward.normalized * 0.23f, Quaternion.identity);
        newBox.transform.parent = collectablesGroup.transform;
    }
}

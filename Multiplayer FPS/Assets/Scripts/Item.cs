using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemScene;
    public GameObject itemHand;
    public Transform socket;

    SphereCollider myCollider;

    private void Start()
    {
        myCollider = GetComponent<SphereCollider>();
    }
    public void Get()
    {
        myCollider.enabled = false;

        itemScene.SetActive(false);
        itemHand.SetActive(true);

        transform.SetParent(socket);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}

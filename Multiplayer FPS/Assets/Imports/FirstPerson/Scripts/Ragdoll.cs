using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Ray ray;
    public LayerMask mask; // Ragdoll
    public GameObject bloodFX;
    public float bulletForce = 1000;

    AudioSource audioDie;

    private void Start()
    {
        audioDie = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 20, mask))
        {
            audioDie.Play();
            hitInfo.collider.GetComponent<Rigidbody>().AddForce(ray.direction * bulletForce);
            Instantiate(bloodFX, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), hitInfo.collider.transform);
            enabled = false;
        }
    }
}

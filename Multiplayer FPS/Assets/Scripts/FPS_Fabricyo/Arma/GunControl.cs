using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public Animator gunAnimator;

    public Animator cameraAnimator;

    public string[] triggers;

    public Camera mainCam;

    public LayerMask mask;

    public Transform origin;

    public GameObject particlePrefab;

    public GameObject bulletHolePrefab;

    public AudioSource shootAudio;

    public GameObject Zombie;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            gunAnimator.SetTrigger("Fire");

            cameraAnimator.SetTrigger(triggers[Random.Range(0, 3)]);

            Shoot();

        }
    }

    public void Shoot()
    {
        Instantiate(particlePrefab, origin.transform.position, origin.transform.rotation);
        shootAudio.pitch = Random.Range(0.9f, 1.1f);
        shootAudio.Play();

        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, mask))
        {
            if (hit.collider.CompareTag("Finish"))
            {
                print("Merda");
            }

            if (hit.collider.CompareTag("Enemy"))
            {
                Instantiate(Zombie, hit.collider.gameObject.transform.position, Quaternion.LookRotation(hit.normal));

                Destroy(hit.collider.gameObject);

            }

            else if (hit.collider.CompareTag("Ragdoll") || hit.collider.CompareTag("Rescue Zone") || hit.collider.CompareTag("Collectable"))
            {

            }

            else
            {

                Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));

            }

        }
    }
}

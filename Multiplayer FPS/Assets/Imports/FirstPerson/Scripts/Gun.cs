using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Image imgTarget;
    public Transform spawnMuzzle;
    public LayerMask mask;
    public GameObject ragdoll;

    public GameObject stoneImpactFX;
    public GameObject muzzleFX;
    public AudioClip shootAudioFX;

    AudioSource audioGun;
    RaycastHit hitInfo;


    private void Start()
    {
        audioGun = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(imgTarget.rectTransform.position);
        Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            audioGun.PlayOneShot(shootAudioFX);
            
            if (spawnMuzzle)
            {
                Instantiate(muzzleFX, spawnMuzzle.position, spawnMuzzle.rotation);
            }

            if (Physics.Raycast(ray, out hitInfo, 1000, mask))
            {
                switch (hitInfo.collider.tag)
                {
                    case "Floor":
                        if (stoneImpactFX)
                        {
                            Instantiate(stoneImpactFX, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        }
                        break;

                    case "Enemy":
                        GameObject tmp = Instantiate(ragdoll, hitInfo.collider.transform.position, hitInfo.collider.transform.rotation);
                        tmp.GetComponent<Ragdoll>().ray = ray;

                        Destroy(hitInfo.collider.gameObject);

                        break;

                    case "Rescue Zone":
                        break;

                    case "Untagged":
                        break;
                }
            }
        }
    }
}

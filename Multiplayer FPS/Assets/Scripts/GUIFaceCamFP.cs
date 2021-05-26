using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIFaceCamFP : MonoBehaviour
{
    public Camera camera;
    public void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }
}

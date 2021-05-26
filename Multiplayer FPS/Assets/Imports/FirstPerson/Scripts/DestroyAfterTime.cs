using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int time;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}

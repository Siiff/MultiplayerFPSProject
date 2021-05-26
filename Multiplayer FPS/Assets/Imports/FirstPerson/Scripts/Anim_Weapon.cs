using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Weapon : MonoBehaviour
{
    Animator anim;
    public float animSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            anim.SetTrigger("reload");
        if (Input.GetKey(KeyCode.Mouse0))
            anim.SetTrigger("fire");
    }
}

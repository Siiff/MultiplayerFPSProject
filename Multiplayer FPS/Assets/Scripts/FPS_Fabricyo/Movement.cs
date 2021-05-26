using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController cc;

    public float[] walkSpeed;

    public int speedStage;

    Vector3 velocity;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGround;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 move = transform.right * hor + transform.forward * ver;

        cc.Move((move * walkSpeed[speedStage]) * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            cc.Move(new Vector3(0, -0.01f, 0));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && speedStage != 0)
        {
            speedStage = 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && speedStage != 0)
        {
            speedStage = 1;
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

    }
}

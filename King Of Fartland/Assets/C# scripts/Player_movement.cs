using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool punch = false;
    bool fart = false;


    void Update () {

        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            punch = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            fart = true;
        }

	}

    private void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        controller.Action(horizontalMove, verticalMove, punch, fart);
        jump = false;
        punch = false;
        fart = false;
    }
}

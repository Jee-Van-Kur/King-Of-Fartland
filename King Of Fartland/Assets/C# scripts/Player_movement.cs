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
    bool kick = false;
    bool hit = false;


    void Update () {

        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Jump"))
        {
            fart = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            punch = true;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            kick = true;
        }

    }

    private void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime);
        controller.Action(horizontalMove, verticalMove, punch, fart, kick, hit);
        jump = false;
        punch = false;
        fart = false;
        kick = false;
        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            hit = true;
        }
    }
}

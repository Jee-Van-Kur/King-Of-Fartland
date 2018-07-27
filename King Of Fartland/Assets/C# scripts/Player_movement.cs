using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_movement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool punch = false;
    bool fart = false;
    bool kick = false;
    bool hit = false;
    public Text fartText;
    public Menu menu;

    private void Start()
    {
        
    }
    void Update () {

        fartText.text = "Fart: " + controller.currenFart.ToString() + "psi"; 
        if(controller.currenFart <= 0f)
            fartText.text = "Reloading";

        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Jump"))
        {
            fart = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            punch = true;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            kick = true;
        }

    }

    private void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime);
        controller.Action(horizontalMove, verticalMove, punch, fart, kick, hit);
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

        if (col.gameObject.tag == "Respawn")
        {
            menu.Pause();
        }
    }

}

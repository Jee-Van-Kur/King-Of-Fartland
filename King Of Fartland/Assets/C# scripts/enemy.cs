using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;

    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    private Animator player_anim;

    public float dist = 10f;
    public float speed = 40f;
    public Transform target;
    private Rigidbody2D m_enemy;
    private bool hit = false;
    private float dir = 0.0f;

    void Start()
    {

        m_enemy = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Move(float move, bool HIT)
    {

        if (m_Grounded || !HIT)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_enemy.velocity.y);
            // And then smoothing it out and applying it to the character
            m_enemy.velocity = Vector3.SmoothDamp(m_enemy.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            if (m_Grounded && move != 0)
                player_anim.Play("enemy_run");
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        if (HIT)
        {
            m_enemy.AddForce(new Vector2(move*(-2000.0f), 0.0f));
            player_anim.Play("enemy_hurt");
            Invoke("Nohit", 0.2f);
        }

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        m_Grounded = false;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }


        float direction = target.transform.position.x - transform.position.x;
        dir = Mathf.Sign(direction);
        if (Mathf.Abs(direction) < dist)
        {
            player_anim.Play("enemy_punch");                                  // punch
        }
        else
            Move(dir * speed * Time.fixedDeltaTime, hit);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hit = true;
            Move(dir, hit);
        }
    }

    private void Nohit()
    {
        hit = false;
    }
}

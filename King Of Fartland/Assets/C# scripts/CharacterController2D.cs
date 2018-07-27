using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{						
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	
	[SerializeField] private bool m_AirControl = false;							
	[SerializeField] private LayerMask m_WhatIsGround;		
	[SerializeField] private Transform m_GroundCheck;

	const float k_GroundedRadius = .2f; 
	private bool m_Grounded;           
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;
    private Animator player_anim;
    public float maxFart = 20f;
    public float currenFart;
    public float Flyforce = 700f;
    public GameObject fartTrail;
    bool isreloading = false;
    private shake ziggy;

    public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
        ziggy = GameObject.FindGameObjectWithTag("screenShake").GetComponent<shake>();
        currenFart = maxFart;
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();
        if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

    private void Update()
    {
        if (isreloading)
            return;

        if (currenFart <= 0.0f)
            StartCoroutine(Reload());
    }
    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}




	public void Move(float move)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            if (m_Grounded && move!=0)
                player_anim.Play("run");
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
    }

    public void Action(float move, float vert, bool punch, bool fart, bool kick, bool hit)
    {
        if (fart && move != 0f && currenFart >= 5.0f)             // special fart
        {
            ziggy.camDash();
            fartTrail.SetActive(true);
            player_anim.Play("Punch");          
            m_Rigidbody2D.AddForce(new Vector2(Mathf.Sign(move) * 1000f, 0f));
            Invoke("Done", 0.5f);
            currenFart -= 5.0f;
        }

        else if (punch)
        {
            player_anim.Play("Punch");            // Punch
        }

        if (vert > 0 && fart && m_Grounded && currenFart >= 10f)                                           // fart fly combo
        {
            ziggy.camJump();
            m_Grounded = false;
            player_anim.Play("hover");
            m_Rigidbody2D.AddForce(new Vector2(0f, Flyforce));
            currenFart -= 10f;
        }

        if (kick)
        {
            player_anim.Play("kick");
        }

        if (hit)
        {
            player_anim.Play("hurt");
            m_Rigidbody2D.AddForce(new Vector2(Mathf.Sign(move) * 1000f, 0.0f));
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

    void Done()
    {
        fartTrail.SetActive(false);
    }

    IEnumerator Reload()
    {
        isreloading = true;
        yield return new WaitForSeconds(10.0f);
        currenFart = maxFart;
        isreloading = false;
    }
}

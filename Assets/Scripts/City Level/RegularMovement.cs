using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMovement : MonoBehaviour
{
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    Rigidbody2D rb;
    [SerializeField] float speed = 200f;
    [SerializeField] float jumpForce = 600f;
    float xMovement;
    float yMovement;

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] bool isGrounded = true;

    [SerializeField] float hangTime = .2f;
    float hangCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = rb.velocity.y;
        rb.velocity = new Vector2(xMovement * speed, yMovement);

        if (rb.velocity.x != 0 && isGrounded)
        {
            GetComponent<Animator>().SetBool("isRunning", true);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Footstep");
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (!isGrounded)
        {
            GetComponent<Animator>().SetBool("isJumping", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isJumping", false);
        }


        isGrounded = Physics2D.Linecast(transform.position, groundCheckLeft.position, whatIsGround) || Physics2D.Linecast(transform.position, groundCheckRight.position, whatIsGround);
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        if (hangCounter > 0 && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Jump");
        }

        // If the input is moving the player right and the player is facing left...
        if (rb.velocity.x > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (rb.velocity.x < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
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
}

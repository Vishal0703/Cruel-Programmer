using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 200f;
    [SerializeField] float jumpForce = 600f;
    float xMovement;
    float yMovement;

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] float checkRadius = .5f;
    [SerializeField] bool isGrounded = true;

    [SerializeField] AudioClip landingSound;
    AudioSource myAudio;

    [SerializeField] float hangTime = .2f;
    float hangCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = rb.velocity.y;
        rb.velocity = new Vector2(xMovement * speed, yMovement);

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
        }
    }
}

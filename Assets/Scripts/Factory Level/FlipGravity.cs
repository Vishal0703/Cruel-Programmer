using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] GameObject flipPrefab;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isJumping", true);
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(xMovement * speed * Time.deltaTime, 0f);
        transform.Translate(movement);
        if (Input.GetButtonDown("Jump"))
        {
            if (flipPrefab != null)
                Instantiate(flipPrefab, (groundCheckLeft.position + groundCheckRight.position) / 2, Quaternion.identity);
            rb.gravityScale *= -1;
            transform.localScale *= -1;
        }
    }
}

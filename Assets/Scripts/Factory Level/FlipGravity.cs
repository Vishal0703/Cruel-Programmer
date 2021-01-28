using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(xMovement * speed * Time.deltaTime, 0f);
        transform.Translate(movement);
        if (Input.GetButtonDown("Jump"))
        {
            rb.gravityScale *= -1;
        }
    }
}

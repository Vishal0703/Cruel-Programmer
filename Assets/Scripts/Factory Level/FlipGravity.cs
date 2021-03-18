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
    public Controls controls;
    private bool jump;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Jump.performed += _ => Jump(true);
        controls.Player.Jump.canceled += _ => Jump(false);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump(bool isJumping)
    {
        jump = isJumping;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isJumping", true);
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = controls.Player.HorizontalMove.ReadValue<float>();
        Vector2 movement = new Vector2(xMovement * speed * Time.deltaTime, 0f);
        transform.Translate(movement);
        if (jump)
        {
            jump = false;
            if (flipPrefab != null)
                Instantiate(flipPrefab, (groundCheckLeft.position + groundCheckRight.position) / 2, Quaternion.identity);
            rb.gravityScale *= -1;
            transform.localScale *= -1;
        }
    }
}

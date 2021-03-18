using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 600f;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] GameObject jumpPrefab;
    bool start = false;


    public Controls controls;
    public bool jump;

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
        if (Time.timeScale == 0f)
            return;
        Vector3 movement = new Vector3(1f, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        if (jump)
        {
            jump = false;
            if (jumpPrefab != null)
                Instantiate(jumpPrefab, (groundCheckLeft.position + groundCheckRight.position) / 2, Quaternion.identity);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Jump");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}

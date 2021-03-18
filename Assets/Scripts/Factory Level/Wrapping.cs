using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapping : MonoBehaviour
{
    Renderer[] renderers;
    Rigidbody2D rb;
    bool isWrappingX = false;
    bool isWrappingY = false;
    [SerializeField] float speed = 50f;
    [SerializeField] float fakeGravity = 20f;
    Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isJumping", true);
    }

    bool CheckRenderers()
    {
        foreach (var renderer in renderers)
        {
            // If at least one render is visible, return true
            if (renderer.isVisible)
            {
                return true;
            }
        }

        // Otherwise, the object is invisible
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0f, -fakeGravity);
        ScreenWrap();
        float xMovement = controls.Player.HorizontalMove.ReadValue<float>();
        Vector2 movement = new Vector2(xMovement * speed * Time.deltaTime, 0f);
        transform.Translate(movement);
    }

    void ScreenWrap()
    { 
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
        }

        transform.position = newPosition;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 600f;
    bool start = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isJumping", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(1f, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Jump");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}

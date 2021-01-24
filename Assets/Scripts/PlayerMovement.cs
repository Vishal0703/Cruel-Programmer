﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	[Range(0.0f, 10.0f)] // create a slider in the editor and set limits on moveSpeed
	public float moveSpeed = 3f;

	public float jumpForce = 600f;

	// LayerMask to determine what is considered ground for the player
	public LayerMask whatIsGround;

	// Transform just below feet for checking if player is grounded
	public Transform groundCheck;
	public AudioClip thud;

	// player can move?
	// we want this public so other scripts can access it but we don't want to show in editor as it might confuse designer
	[HideInInspector]
	public bool playerCanMove = true;




	// hold player motion in this timestep
	float _vx;
	float _vy;

	// player tracking
	bool facingRight = true;
	bool isGrounded = false;
	bool isRunning = false;


	Rigidbody2D rigidbody;
	AudioSource audio;
	//Rigidbody2D rgb;
	Vector3 orig_pos;
	// store the layer the player is on (setup in Awake)
	int _playerLayer;

	// number of layer that Platforms are on (setup in Awake)
	int _platformLayer;

	void Awake()
	{
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		

		rigidbody = GetComponent<Rigidbody2D>();
		if (rigidbody == null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");

		audio = GetComponent<AudioSource>();
		// determine the player's specified layer
		_playerLayer = this.gameObject.layer;

		// determine the platform's specified layer
		_platformLayer = LayerMask.NameToLayer("Platform");
	}

	// this is where most of the player controller magic happens each game event loop
	void Update()
	{
		// exit update if player cannot move or game is paused
		if (!playerCanMove || (Time.timeScale == 0f))
			return;

		// determine horizontal velocity change based on the horizontal input
		_vx = Input.GetAxisRaw("Horizontal");

		// Determine if running based on the horizontal movement
		if (_vx != 0)
		{
			isRunning = true;
		}
		else
		{
			isRunning = false;
		}


		// get the current vertical velocity from the rigidbody component
		_vy = rigidbody.velocity.y;

		// Check to see if character is grounded by raycasting from the middle of the player
		// down to the groundCheck position and see if collected with gameobjects on the
		// whatIsGround layer
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);

		if (GameManager.gm.isJumpAvailable)
		{
			if (isGrounded && Input.GetButtonDown("Jump")) // If grounded AND jump button pressed, then allow the player to jump
			{
				DoJump();
			}

			// If the player stops jumping mid jump and player is not yet falling
			// then set the vertical velocity to 0 (he will start to fall from gravity)
			if (Input.GetButtonUp("Jump") && _vy > 0f)
			{
				_vy = -1f;
			}
		}

		// Change the actual velocity on the rigidbody
		if (!GameManager.gm.isleftAvailable && _vx < 0f)
			_vx = 0f;

		rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy);
		

		// if moving up then don't collide with platform layer
		// this allows the player to jump up through things on the platform layer
		// NOTE: requires the platforms to be on a layer named "Platform"
		//Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, (_vy > 0.0f));
		if ((_vx > 0) || (_vx == 0 && transform.localScale.x > 0))
			facingRight = true;

		else if ((_vx < 0) || (_vx == 0 && transform.localScale.x < 0))
			facingRight = false;

	}

	void DoJump()
	{
		// reset current vertical motion to 0 prior to jump
		_vy = 0f;
		// add a force in the up direction
		rigidbody.AddForce(new Vector2(0, jumpForce));
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		//if (rigidbody.velocity.y < 0f)
		//	audio.PlayOneShot(thud);

		if (collision.gameObject.CompareTag("ground"))
			audio.PlayOneShot(thud);

		if (collision.gameObject.CompareTag("obstacle"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("goal"))
		{
			Debug.Log("Victory");
			if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
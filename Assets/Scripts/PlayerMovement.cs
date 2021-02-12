using System.Collections;
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
	public GameObject jumpPrefab;



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


	//Rigidbody2D rigidbody;
	AudioSource audio;
	Rigidbody2D rgbd;
	Animator anim;
	Vector3 orig_pos;
	// store the layer the player is on (setup in Awake)
	int _playerLayer;

	// number of layer that Platforms are on (setup in Awake)
	int _platformLayer;

	void Awake()
	{
        // get a reference to the components we are going to be changing and store a reference for efficiency purposes


        rgbd = GetComponent<Rigidbody2D>();
        if (rgbd == null) // if Rigidbody is missing
            Debug.LogError("Rigidbody2D component missing from this gameobject");

        audio = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
        // determine the player's specified layer
        _playerLayer = this.gameObject.layer;

		// determine the platform's specified layer
		_platformLayer = LayerMask.NameToLayer("Platform");
	}

    private void Start()
    {
		if (GameManager.gm.isGravityReversed)
			facingRight = !facingRight;
    }
    // this is where most of the player controller magic happens each game event loop
    void Update()
	{
		//anim.SetBool("isJumping", false);
		//anim.SetBool("isVictory", false);
		//anim.SetBool("isDead", false);
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

		if (GameManager.gm.isGravityReversed)
			_vx = -_vx;
		if (GameManager.gm.isMotionReversed)
			_vx = -_vx;
		// get the current vertical velocity from the rigidbody component
		_vy = rgbd.velocity.y;


		// Check to see if character is grounded by raycasting from the middle of the player
		// down to the groundCheck position and see if collected with gameobjects on the
		// whatIsGround layer
		if (GameManager.gm.isAirJumpAllowed)
			isGrounded = true;
		else
			isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);

		

		// Change the actual velocity on the rigidbody
		if (!GameManager.gm.isLeftAvailable && _vx < 0f)
			_vx = 0f;
		if (!GameManager.gm.isRightAvailable && _vx > 0f)
			_vx = 0f;


		if (_vx != 0)
		{
			anim.SetBool("isRunning", true);
			FMODUnity.RuntimeManager.PlayOneShot("event:/Footstep");
		}
		else
			anim.SetBool("isRunning", false);


		Vector2 dir = new Vector2(0f,1f);
		if (!GameManager.gm.isCircularLevel)
			rgbd.velocity = new Vector2(_vx * moveSpeed, _vy);
		else
		{
			if (GameManager.gm.center == null)
				Debug.LogError("Center Level but no center object assigned");
			else
			{
				dir = GameManager.gm.center.position - transform.position;
				dir = dir.normalized;
				if (dir.magnitude != 0)
				{
					float sin_theta = dir.y / dir.magnitude;
					float cos_theta = dir.x / dir.magnitude;
					Vector2 vel = new Vector2();
					vel.x = rgbd.velocity.x + _vx * moveSpeed * sin_theta;
					vel.y = rgbd.velocity.y - _vx * moveSpeed * cos_theta;
					rgbd.velocity = vel;
					RotatePlayer(dir);
				}
			}
		}

		if (!GameManager.gm.isControlGravity)
		{
			if (GameManager.gm.isJumpAvailable)
			{
				if (isGrounded && Input.GetButtonDown("Jump")) // If grounded AND jump button pressed, then allow the player to jump
				{
					DoJump(dir);
				}

				// If the player stops jumping mid jump and player is not yet falling
				// then set the vertical velocity to 0 (he will start to fall from gravity)
				if (Input.GetButtonUp("Jump") && _vy > 0f)
				{
					_vy = -1f;
				}
			}
		}
		else
        {
			if (Input.GetButtonDown("Jump"))
			{
				GameManager.gm.isGravityReversed = !GameManager.gm.isGravityReversed;
			}
		}

		// if moving up then don't collide with platform layer
		// this allows the player to jump up through things on the platform layer
		// NOTE: requires the platforms to be on a layer named "Platform"
		//Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, (_vy > 0.0f));
		//if ((_vx > 0) || (_vx == 0 && transform.localScale.x > 0))
		//	facingRight = true;

		//else if ((_vx < 0) || (_vx == 0 && transform.localScale.x < 0))
		//	facingRight = false;

		//if (facingRight)
		//{
		//	var thescale = transform.localScale;
		//	thescale.x *= 1;
		//	transform.localScale = thescale;
		//}
		//else
  //      {
		//	var thescale = transform.localScale;
		//	thescale.x *= -1;
		//	transform.localScale = thescale;
		//}

		if(_vx > 0 && !facingRight)
        {
			Flip();
        }
		else if(_vx<0 && facingRight)
        {
			Flip();
        }

	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void RotatePlayer(Vector2 dir)
    {
		if(!GameManager.gm.isGravityReversed)
			dir = -dir;
		float theta = Mathf.Acos(Vector2.Dot(dir, new Vector2(0f, 1f))/(dir.magnitude));
		theta = theta * 180 / Mathf.PI;
		theta = 180 - theta;
        if (dir.x <= 0)
            theta = -theta;
        transform.eulerAngles = new Vector3(0f, 0f, theta);
    }

	void DoJump(Vector2 dir)
	{
		anim.SetBool("isJumping", true);
		anim.SetTrigger("jumpTrigger");
		if (GameManager.gm.isGravityReversed)
			dir = -dir;
		Debug.Log($"{groundCheck.rotation}");
		if(jumpPrefab != null)
			Instantiate(jumpPrefab, groundCheck.position, groundCheck.rotation);
		FMODUnity.RuntimeManager.PlayOneShot("event:/Jump");
		// reset current vertical motion to 0 prior to jump
		//_vy = 0f;
		//float tan_theta = dir.y / dir.x;
		//float vx1 = rgbd.velocity.x;
		//float vy1 = rgbd.velocity.y;
		//vx1 = -tan_theta * vy1;
		//rgbd.velocity = new Vector2(vx1, vy1);
		// add a force in the up direction
		rgbd.AddForce(dir.normalized*jumpForce);
		if(GameManager.gm.isJumpCountRestricted)
			GameManager.gm.restrictedJumpCount--;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		//if (rigidbody.velocity.y < 0f)
		//	audio.PlayOneShot(thud);

		//if (collision.gameObject.CompareTag("ground"))
		//	GetComponent<AudioSource>().PlayOneShot(thud);

		if (collision.gameObject.CompareTag("obstacle"))
		{
			anim.SetBool("isDead", true);
			anim.SetTrigger("deadTrigger");
			FMODUnity.RuntimeManager.PlayOneShot("event:/Hurt");
			rgbd.velocity = new Vector2(0f, 0f);
			transform.GetComponent<BoxCollider2D>().enabled = false;
			GameManager.gm.LevelSelect(SceneManager.GetActiveScene().buildIndex, 1.2f);
		}
		
			
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("goal"))
		{
			Debug.Log("Victory");
			anim.SetTrigger("victoryTrigger");
			anim.SetBool("isVictory", true);
			FMODUnity.RuntimeManager.PlayOneShot("event:/Goal");
			ResetPlayer();
			if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
				GameManager.gm.LevelSelect(SceneManager.GetActiveScene().buildIndex + 1, 1.5f);
		}
	}

	void ResetPlayer()
    {
		transform.position = new Vector3(0f, 0f, 0f);
		transform.eulerAngles = new Vector3(0f, 0f, 0f);
		rgbd.velocity = new Vector2(0f, 0f);
	}
}

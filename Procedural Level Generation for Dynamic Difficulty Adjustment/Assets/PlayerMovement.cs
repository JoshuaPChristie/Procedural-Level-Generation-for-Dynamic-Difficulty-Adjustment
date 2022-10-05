using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Sources:
    //Basic Controls - https://www.youtube.com/watch?v=QGDeafTx5ug
    //Variable Jump - https://www.youtube.com/watch?v=j111eKN8sJw
    //Game Feel Improvements - https://www.youtube.com/watch?v=8QPmhDYn6rk


    private Rigidbody2D rb;

    //For Movement
    public int moveSpeed;
    public int jumpSpeed;

    private float moveInput;

    //For Jumping
    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;

    private bool isJumping = false;

    public float hangTime;
    private float hangTimeCounter;

    public float jumpBufferTime;
    private float jumpBufferCounter;

    //For Camera
    public Transform camTarget;
    public float leadDist, leadSpeed;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check ground collision
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Hang time
        if (isGrounded)
        {
            hangTimeCounter = hangTime;
        }
        else
        {
            hangTimeCounter -= Time.deltaTime;
        }

        //Horizontal movement
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        //Move Camera
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, leadDist * Input.GetAxisRaw("Horizontal"), leadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }

    void Update()
    {
        //Jump buffer
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //Jump when key pressed
        //if (Input.GetKeyDown(KeyCode.W) && hangTimeCounter > 0)
        if (jumpBufferCounter > 0 && hangTimeCounter > 0)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            jumpBufferCounter = 0;
            rb.velocity = Vector2.up * jumpSpeed;
        }

        //Jump higher if key is held
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //Stop jumping when key released
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }
}

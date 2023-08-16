using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Check out Screen Wrapping
    [Header("Movement System")]
    [SerializeField] private float moveSpeed = 1f;

    [Header("Jump System")]
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] float fallMultiplier = 3f;
    [SerializeField] private float jumpTime = 0.4f;
    [SerializeField] private float jumpMultiplier = 2.0f;

    // Components
    private Rigidbody2D rb;
    private Animator myAnimator;

    // Movement System
    private Vector2 moveInput;

    // Jump System
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 vecGravity;
    bool isJumping;
    float jumpCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    private void Update()
    {
        Move();
        FlipSprite();
        Jump();        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            myAnimator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isJumping = true;
            jumpCounter = 0;
        }

        // Holding jump jumps higher
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            myAnimator.SetBool("IsJumping", false);
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

        // Falls faster depending on fallMultiplier
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.53f, 0.045f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Attack");
            myAnimator.SetTrigger("Attack");
        }
    }

    private void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool isMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", isMovingHorizontally);
    }

    private void FlipSprite()
    {
        bool isMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (isMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    // Delete these methods
    public void SetJumpTime(float newJumpTime)
    {
        jumpTime = newJumpTime;
    }

    public void SetJumpPower(float newJumpPower)
    {
        jumpMultiplier = newJumpPower;
    }
}
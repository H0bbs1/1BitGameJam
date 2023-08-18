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

    [Header("Misc")]
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private CapsuleCollider2D bodyCollider;
    [SerializeField] private Vector2 deathKick = new Vector2(0, 20.0f);
    public ScreenBounds screenBounds;

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

    // Misc
    bool isAlive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Move();
        FlipSprite();
        Jump();
        Die();
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
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.53f, 0.034f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && isAlive)
        {
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
        }
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        // Screen Wrap
        if (screenBounds.AmIOutOfBounds(transform.position))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(transform.position);
            transform.position = newPosition;
        }

        bool isMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", isMovingHorizontally);
        myAnimator.SetFloat("moveX", playerVelocity.x);
    }

    private void FlipSprite()
    {
        bool isMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (isMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            bodyCollider.enabled = false;
            groundCheck.gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 90);

            // Turn off all animations
            myAnimator.SetBool("IsRunning", false);

            rb.velocity = deathKick;
        }
    }
}
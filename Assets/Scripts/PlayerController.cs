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

    [Header("Attack System")]
    [SerializeField] AudioClip attackAudio;
    [SerializeField] private Transform weaponCollider;

    [Header("Misc")]
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
    public bool isAlive = true;
    bool isAttacking = false;
    private EnemySpawner enemySpawner;
    private StageController stageController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        stageController = FindObjectOfType<StageController>();
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
        Switch();
        Die();
    }

    void Switch()
    {
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            stageController.SwitchColors();
        }
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
        if (value.isPressed && isAlive && !isAttacking)
        {
            isAttacking = true;
            AudioSource.PlayClipAtPoint(attackAudio, Camera.main.transform.position, 0.5f);
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
        }
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
        isAttacking = false;
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

    public void TriggerDie()
    {
        Die();
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hole")))
        {
            isAlive = false;
            gameObject.layer = LayerMask.NameToLayer("Dead");
            rb.velocity = Vector2.zero;

            // Turn off animation
            myAnimator.SetBool("IsRunning", false);
            myAnimator.SetTrigger("Die");

            // Turn off spawner
            enemySpawner.StopSpawner();
        }
    }
}
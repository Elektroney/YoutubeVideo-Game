using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Transforms & Colliders")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private Collider2D crouchingCollider;

    [Header("Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] walkSprites;

    [Header("Events")]
    public UnityEvent OnLandEvent;
    public UnityEvent<bool> OnCrouchEvent;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;
    private bool isWalking;
    private float horizontalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new UnityEvent<bool>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();


        Animate();
    }

    private void FixedUpdate()
    {
        Move(horizontalInput * speed * (isCrouching ? crouchSpeedMultiplier : 1f));
        CheckGrounded();
    }

    private void Move(float move)
    {
        Vector2 targetVelocity = new Vector2(move, rb.velocity.y);
        rb.velocity = targetVelocity;

        isWalking = Mathf.Abs(move) > 0;

        // Flip the sprite if walking left
        if (move < 0)
            spriteRenderer.flipX = true;
        else if (move > 0)
            spriteRenderer.flipX = false;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        OnCrouchEvent.Invoke(isCrouching);

        standingCollider.enabled = !isCrouching;
        crouchingCollider.enabled = isCrouching;
    }

private void CheckGrounded()
{
    RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, whatIsGround);
    isGrounded = hit.collider != null && hit.collider.CompareTag("Block");

    if (!isGrounded)
        OnLandEvent.Invoke();
}

    private void Animate()
    {
        if (isWalking )
            AnimateWalk();
        else
            AnimateIdle();
    }

    private void AnimateWalk()
    {
        int frameIndex = Mathf.FloorToInt(Time.time * 10f) % walkSprites.Length;
        spriteRenderer.sprite = walkSprites[frameIndex];
    }

    private void AnimateIdle()
    {
        int frameIndex = Mathf.FloorToInt(Time.time * 5f) % idleSprites.Length;
        spriteRenderer.sprite = idleSprites[frameIndex];
    }
}

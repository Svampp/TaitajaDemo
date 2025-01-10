using UnityEngine;

/// <summary>
/// Handles player movement, including jumping, running, and collision with enemies.
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // Speed and movement parameters
    public float moveSpeed = 5f;          // Speed of movement
    public float jumpForce = 7f;         // Jump power

    // Ground detection settings
    public LayerMask groundLayer;        // Layer to check for ground collisions
    public Transform groundCheck;        // Position used to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius of the ground check

    // Cached references to components
    Rigidbody2D rb;
    Animator animator;
    PlayerHealth playerHealth;

    // Control flag
    bool isControllable = true;  // Determines if the player can move or perform actions

    void Start()
    {
        // Cache references to components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Stop all actions if the player is not controllable
        if (!isControllable) return;

        // Check if the player is on the ground
        bool isGrounded = IsGrounded();

        // Get horizontal input and update the player's velocity
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player's sprite based on movement direction
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        // Jump if spacebar is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Update animator parameters for grounded status and movement
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
    }

    /// <summary>
    /// Draws a visual representation of the ground check radius in the editor.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    /// <summary>
    /// Handles collisions with enemies. If the player collides with an enemy, they die.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isControllable = false; // Disable player controls
            playerHealth.Die();     // Trigger the death process
        }
    }

    /// <summary>
    /// Checks if the player is currently on the ground.
    /// </summary>
    /// <returns>True if the player is grounded, otherwise false.</returns>
    bool IsGrounded() =>
        Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    /// <summary>
    /// Resets control of the player, allowing them to move again.
    /// Typically called after a respawn.
    /// </summary>
    public void ResetControl()
    {
        isControllable = true; // Re-enable player controls
    }
}

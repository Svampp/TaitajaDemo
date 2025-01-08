using UnityEngine;

/// <summary>
/// Player movement script
/// </summary>
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed of movement
    public float jumpForce = 7f;         // Jump power
    public LayerMask groundLayer;        // Layer of ground for checking
    public Transform groundCheck;        // Ground touch test point
    public float groundCheckRadius = 0.2f; // Ground touch check radius

    Rigidbody2D rb;
    Animator animator;
    bool isControllable = true;  // Flag to check if the player can be controlled

    void Start()
    {
        // Cache references to Rigidbody2D and Animator components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isControllable) return; // Stop if player controls are disabled

        // Check if the player is grounded
        bool isGrounded = IsGrounded();

        // Get horizontal input and update velocity
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player sprite based on movement direction
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        // Jump if the spacebar is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Update animator parameters
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
    }

    /// <summary>
    /// Draw the ground check radius in the editor
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
    /// Check if the player collides with an object tagged as "Enemy" and player die
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isControllable = false; // Disable player controls
            rb.velocity = Vector2.zero; // Stop all movement
            animator.SetTrigger("Death"); // Trigger death animation
            Invoke(nameof(DisablePlayer), 1f); // Disable player after 1 second
        }
    }

    /// <summary>
    /// Check if the player is touching the ground
    /// </summary>
    /// <returns>True if grounded, false otherwise</returns>
    bool IsGrounded() =>
        Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    /// <summary>
    /// Disable the player object
    /// </summary>
    void DisablePlayer() => gameObject.SetActive(false);
}

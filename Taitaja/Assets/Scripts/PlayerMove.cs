using UnityEngine;
/// <summary>
/// Handles player movement, including jumping, running, and collision with enemies.
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // Speed and movement parameters
    public float moveSpeed = 5f; // Speed of horizontal movement
    public float jumpForce = 7f; // Force applied when the player jumps

    // Ground detection settings
    public LayerMask groundLayer; // Layer used to determine what is ground
    public Transform groundCheck; // Position used to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius of the ground detection circle

    // Cached references to components
    Rigidbody2D rb;
    Animator animator;
    bool isControllable = true; // Flag to disable controls when needed

    PlayerHealth playerHealth; // Reference to the player's health component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache Rigidbody2D component
        animator = GetComponent<Animator>(); // Cache Animator component
        playerHealth = GetComponent<PlayerHealth>(); // Cache PlayerHealth component
    }

    void Update()
    {
        if (!isControllable) return; // Exit if controls are disabled

        // Check if the player is grounded
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal"); // Get input (-1, 0, 1)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Apply horizontal velocity

        // Flip the player's sprite based on movement direction
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply vertical velocity
        }

        // Update animator parameters for grounded status and movement
        animator.SetBool("isGrounded", isGrounded); // Update grounded status
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x)); // Update horizontal velocity
        animator.SetFloat("velocityY", rb.velocity.y); // Update vertical velocity
    }

    /// <summary>
    /// Resets control of the player, allowing movement again.
    /// </summary>
    public void ResetControl()
    {
        isControllable = true; // Re-enable player controls
    }

    /// <summary>
    /// Handles collisions with enemies. Disables controls and triggers death logic.
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) // Check if the collided object is tagged as "Enemy"
        {
            isControllable = false; // Disable player controls
            playerHealth?.Die(); // Trigger the death process if PlayerHealth is available
        }
    }

}
using UnityEngine;
/// <summary>
/// Player movement script
/// </summary>
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed ​​of movement
    public float jumpForce = 7f;        // Jump power
    public LayerMask groundLayer;        // Layer of ground for checking
    public Transform groundCheck;       // Ground touch test point
    public float groundCheckRadius = 0.2f; // Ground touch check radius

    Rigidbody2D rb;
    Animator animator;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ground touch check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Receiving player input
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Moving
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Player turn
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Animations: updating animator parameters
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
    }

    void OnDrawGizmosSelected()
    {
        // Drawing the ground check radius in the editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

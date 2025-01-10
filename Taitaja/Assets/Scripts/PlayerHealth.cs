using System.Collections;
using UnityEngine;

/// <summary>
/// Handles player health, including death, animations, and respawn logic.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    // References to components
    Animator animator;
    Rigidbody2D rb;

    // Respawn point and delay before respawning
    public Transform respawnPoint;
    public float respawnDelay = 1f;

    // Reference to the player movement script
    PlayerMove playerMove;

    // Flag to prevent multiple respawn triggers
    bool isRespawning;

    void Start()
    {
        // Initialize references
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    /// <summary>
    /// Handles the player's death logic, including stopping movement,
    /// playing the death animation, and initiating the respawn process.
    /// </summary>
    public void Die()
    {
        // Stop player movement
        rb.velocity = Vector2.zero;

        // Trigger death animation
        animator.SetTrigger("Death");

        // Disable player movement
        if (playerMove != null)
        {
            playerMove.enabled = false;
        }

        // Start the death animation coroutine
        StartCoroutine(HandleDeathAnimation());
    }

    /// <summary>
    /// Coroutine to handle death animation and respawn logic.
    /// </summary>
    IEnumerator HandleDeathAnimation()
    {
        // Set death state in the animator
        animator.SetBool("isDead", true);
        animator.SetTrigger("Death");

        // Wait for the death animation to start
        yield return WaitForAnimationToPlay("Death");

        // Wait for the death animation to finish
        float deathAnimationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationDuration);

        // Deactivate the player
        gameObject.SetActive(false);

        // Trigger respawn through the GameManager
        GameManager.Instance.RespawnPlayer(this.gameObject, respawnDelay);
    }

    /// <summary>
    /// Respawns the player at the current respawn point and resets their state.
    /// </summary>
    public void Respawn()
    {
        // Get the current respawn point from the RespawnManager
        Transform respawnPoint = RespawnManager.Instance.GetCurrentRespawnPoint();

        if (respawnPoint != null)
        {
            // Move the player to the respawn point
            transform.position = respawnPoint.position;

            // Reset the player's velocity
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
        else
        {
            // Exit if no respawn point is available
            return;
        }

        // Reset animator states
        animator.SetBool("isDead", false);
        animator.ResetTrigger("Death");
        animator.SetBool("isGrounded", true);
        animator.SetFloat("velocityX", 0);
        animator.SetFloat("velocityY", 0);

        // Reactivate the player GameObject
        gameObject.SetActive(true);

        // Re-enable player movement
        if (playerMove != null)
        {
            playerMove.enabled = true;
        }

        // Mark respawn process as complete
        isRespawning = false;
    }

    /// <summary>
    /// Waits until a specified animation starts playing.
    /// </summary>
    /// <param name="animationName">The name of the animation to wait for.</param>
    /// <returns>An IEnumerator for coroutine handling.</returns>
    IEnumerator WaitForAnimationToPlay(string animationName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null; // Wait until the next frame
        }
    }

    /// <summary>
    /// Resets the player's state, including animations and movement.
    /// </summary>
    public void ResetState()
    {
        // Reset animator parameters
        animator.SetBool("isDead", false);
        animator.ResetTrigger("Death");
        animator.SetBool("isGrounded", true);
        animator.SetFloat("velocityX", 0);
        animator.SetFloat("velocityY", 0);

        // Re-enable player movement
        if (playerMove != null)
        {
            playerMove.enabled = true;
            playerMove.ResetControl();
        }
    }
}

using System.Collections;
using UnityEngine;

/// <summary>
/// Handles player health, including death, animations, and respawn logic.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    // Cached references to components
    Animator animator; // Animator component for handling animations
    Rigidbody2D rb; // Rigidbody2D component for controlling physics
    PlayerMove playerMove; // Reference to the PlayerMove script

    void Start()
    {
        animator = GetComponent<Animator>(); // Cache Animator component
        rb = GetComponent<Rigidbody2D>(); // Cache Rigidbody2D component
        playerMove = GetComponent<PlayerMove>(); // Cache PlayerMove component
    }

    /// <summary>
    /// Handles the player's death logic, including stopping movement and playing the death animation.
    /// </summary>
    public void Die()
    {
        rb.velocity = Vector2.zero; // Stop the player's movement
        animator.SetTrigger("Death"); // Trigger the death animation
        playerMove.enabled = false; // Disable player movement

        StartCoroutine(HandleDeathAnimation()); // Start coroutine for handling post-death logic
    }

    /// <summary>
    /// Coroutine to handle the death animation and respawn logic.
    /// </summary>
    IEnumerator HandleDeathAnimation()
    {
        animator.SetBool("isDead", true); // Set the isDead parameter for the animator

        // Wait for the current animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false); // Deactivate the player GameObject
        GameManager.Instance.RespawnPlayer(gameObject, 1f); // Trigger respawn through GameManager
    }

    /// <summary>
    /// Resets the player's state, enabling movement and resetting animation parameters.
    /// </summary>
    public void ResetState()
    {
        animator.SetBool("isDead", false); // Reset the isDead parameter
        animator.ResetTrigger("Death"); // Reset the Death trigger
        playerMove.enabled = true; // Re-enable player movement
        playerMove.ResetControl(); // Reset control in the PlayerMove script
    }
}
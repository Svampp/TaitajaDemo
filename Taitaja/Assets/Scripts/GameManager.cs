using UnityEngine;
using System.Collections;

/// <summary>
/// GameManager handles global game logic, including respawning the player after death.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    public static GameManager Instance;

    /// <summary>
    /// Ensures only one instance of the GameManager exists in the game.
    /// Destroys duplicate instances if they are created.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            // Assign this instance to the static Instance variable
            Instance = this;
        }
        else
        {
            // Destroy duplicate GameManager instances
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initiates the respawn process for the player after a delay.
    /// </summary>
    /// <param name="player">The player GameObject to respawn.</param>
    /// <param name="delay">The delay in seconds before the respawn occurs.</param>
    public void RespawnPlayer(GameObject player, float delay)
    {
        // Start a coroutine to handle the respawn process
        StartCoroutine(RespawnCoroutine(player, delay));
    }

    /// <summary>
    /// Coroutine to respawn the player after a specified delay.
    /// </summary>
    /// <param name="player">The player GameObject to respawn.</param>
    /// <param name="delay">The delay in seconds before the respawn occurs.</param>
    private IEnumerator RespawnCoroutine(GameObject player, float delay)
    {
        // Wait for the specified delay before respawning
        yield return new WaitForSeconds(delay);

        // Get the current respawn point from the RespawnManager
        Transform respawnPoint = RespawnManager.Instance.GetCurrentRespawnPoint();

        // If no respawn point is set, exit the coroutine
        if (respawnPoint == null)
        {
            yield break;
        }

        // Set the player's position to the respawn point
        player.transform.position = respawnPoint.position;

        // Reactivate the player GameObject
        player.SetActive(true);

        // Reset the player's state (e.g., health, animations)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetState();
        }
    }
}

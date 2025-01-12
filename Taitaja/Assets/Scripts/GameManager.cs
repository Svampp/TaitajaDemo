using System.Collections;
using UnityEngine;

/// <summary>
/// GameManager handles global game logic, including respawning the player after death.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign this instance to the static Instance variable
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }

    /// <summary>
    /// Initiates the respawn process for the player after a delay.
    /// </summary>
    /// <param name="player">The player GameObject to respawn.</param>
    /// <param name="delay">The delay in seconds before the respawn occurs.</param>
    public void RespawnPlayer(GameObject player, float delay)
    {
        StartCoroutine(RespawnCoroutine(player, delay)); // Start the respawn coroutine
    }

    /// <summary>
    /// Coroutine to respawn the player after a specified delay.
    /// </summary>
    /// <param name="player">The player GameObject to respawn.</param>
    /// <param name="delay">The delay in seconds before the respawn occurs.</param>
    IEnumerator RespawnCoroutine(GameObject player, float delay)
    {
        // Wait for the specified delay before respawning
        yield return new WaitForSeconds(delay);

        // Get the current respawn point from the RespawnManager
        Transform respawnPoint = RespawnManager.Instance.GetCurrentRespawnPoint();
        if (respawnPoint == null) yield break; // Exit if no respawn point is available

        // Set the player's position to the respawn point
        player.transform.position = respawnPoint.position;

        // Reactivate the player GameObject
        player.SetActive(true);

        // Reset the player's state (e.g., health, animations)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth?.ResetState(); // Use null conditional operator to safely call ResetState
    }
}
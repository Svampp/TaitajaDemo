using UnityEngine;

/// <summary>
/// Manages the respawn points for the playerTransform and ensures the correct respawn point is used upon death.
/// </summary>
public class RespawnManager : MonoBehaviour
{
    // Singleton instance of the RespawnManager
    public static RespawnManager Instance;

    // The current active respawn point where the playerTransform will respawn after death
    Transform currentRespawnPoint;

    /// <summary>
    /// Ensures only one instance of the RespawnManager exists in the game.
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
            // Destroy duplicate RespawnManager instances
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets the current respawn point to the specified Transform.
    /// This is usually called when the playerTransform reaches a new checkpoint or transitions to a new map.
    /// </summary>
    /// <param name="newRespawnPoint">The new respawn point for the playerTransform.</param>
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        currentRespawnPoint = newRespawnPoint;
    }

    /// <summary>
    /// Gets the current respawn point where the playerTransform will respawn after death.
    /// </summary>
    /// <returns>The Transform of the current respawn point.</returns>
    public Transform GetCurrentRespawnPoint()
    {
        return currentRespawnPoint;
    }
}

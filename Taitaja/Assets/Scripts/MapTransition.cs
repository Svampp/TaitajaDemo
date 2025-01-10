using UnityEngine;

/// <summary>
/// Handles transitions between maps, updating respawn points and extending camera boundaries.
/// </summary>
public class MapTransition : MonoBehaviour
{
    // The new map to transition to
    public GameObject newMap;

    // The current map being left
    public GameObject currentMap;

    // The current respawn point for the player
    public Transform currentRespawnPoint;

    // The new respawn point for the player after transitioning
    public Transform newRespawnPoint;

    /// <summary>
    /// Triggered when a collider enters the transition zone.
    /// Checks if the player is transitioning to a new map and updates the state accordingly.
    /// </summary>
    /// <param name="collision">The collider entering the trigger zone.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is the one entering the transition zone
        if (collision.CompareTag("Player"))
        {
            // Extend the camera's boundaries to include the new map
            var mainCamera = Camera.main.GetComponent<MainCamera>();
            mainCamera.ExtendBound(currentMap, newMap);

            // Update the respawn point to the new respawn point for this map
            RespawnManager.Instance.SetRespawnPoint(newRespawnPoint);

            // Activate the new map if it is not already active
            if (!newMap.activeSelf)
            {
                newMap.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Updates the current respawn point to a new one.
    /// This method can be used externally to manually set the respawn point.
    /// </summary>
    /// <param name="newRespawnPoint">The new respawn point for the player.</param>
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        currentRespawnPoint = newRespawnPoint;
    }
}

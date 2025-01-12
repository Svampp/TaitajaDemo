using UnityEngine;
/// <summary>
/// Handles transitions between maps, updating respawn points and extending camera boundaries.
/// </summary>
public class MapTransition : MonoBehaviour
{
    // The new map to activate when transitioning
    public GameObject newMap;

    // The new respawn point for the player after transitioning
    public Transform newRespawnPoint;

    /// <summary>
    /// Checks if the player is transitioning to a new map and updates the state accordingly.
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ensure the collision is with the player
        if (!collision.CompareTag("Player")) return;

        // Access the main camera component
        MainCamera mainCamera = Camera.main.GetComponent<MainCamera>();

        // Check if the main camera and the new map are valid
        if (mainCamera != null && newMap != null)
        {
            // Retrieve the map configuration for the new map
            var newMapConfig = newMap.GetComponent<SuperTiled2Unity.SuperMap>();
            if (newMapConfig != null)
            {
                // Extend camera boundaries to include the new map
                mainCamera.ExtendBound(GameObject.Find("Map1"), newMap);
            }
        }

        // Update the respawn point to the new one
        RespawnManager.Instance.SetRespawnPoint(newRespawnPoint);

        // Activate the new map if it is not already active
        if (!newMap.activeSelf) newMap.SetActive(true);
    }
}
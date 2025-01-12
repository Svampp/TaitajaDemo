using UnityEngine;

/// <summary>
/// Controls the main camera's behavior, including following the player and adjusting boundaries to fit the map.
/// </summary>
public class MainCamera : MonoBehaviour
{
    Transform target; // The player's transform that the camera follows
                      // Variables defining the camera boundaries
    float topLeftX;   // Left boundary of the camera
    float topLeftY;   // Top boundary of the camera
    float bottomRightX; // Right boundary of the camera
    float bottomRightY; // Bottom boundary of the camera

    void Start()
    {
        // Initialize boundaries for the first map
        GameObject map = GameObject.Find("Map1");
        if (map != null)
        {
            SetBound(map); // Set the camera's boundaries based on the map
        }
    }

    void Awake()
    {
        // Find the player object by tag and set the target transform for the camera to follow
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void LateUpdate()
    {
        // Ensure the camera follows the player within the set boundaries
        if (target != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(target.position.x, topLeftX, bottomRightX), // Clamp X position
                Mathf.Clamp(target.position.y, bottomRightY, topLeftY), // Clamp Y position
                transform.position.z // Keep Z position unchanged
            );
        }
    }

    /// <summary>
    /// Sets the camera's boundaries based on the map size.
    /// </summary>
    /// <param name="map">The map GameObject to define boundaries.</param>
    public void SetBound(GameObject map)
    {
        // Retrieve the map configuration component
        var config = map.GetComponent<SuperTiled2Unity.SuperMap>();
        if (config == null)
        {
            return;
        }

        // Calculate the camera's view dimensions based on its size and aspect ratio
        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Set camera boundaries using the map's position and dimensions
        topLeftX = map.transform.position.x + aspectRatio;
        topLeftY = map.transform.position.y - cameraSize;
        bottomRightX = map.transform.position.x + config.m_Width - aspectRatio;
        bottomRightY = map.transform.position.y - config.m_Height + cameraSize;
    }

    /// <summary>
    /// Extends the camera boundaries to include the new map when transitioning between maps.
    /// </summary>
    /// <param name="currentMap">The current map GameObject.</param>
    /// <param name="newMap">The new map GameObject.</param>
    public void ExtendBound(GameObject currentMap, GameObject newMap)
    {
        // Retrieve the map configuration components for both maps
        var currentConfig = currentMap.GetComponent<SuperTiled2Unity.SuperMap>();
        var newConfig = newMap.GetComponent<SuperTiled2Unity.SuperMap>();

        if (currentConfig == null || newConfig == null)
        {
            return;
        }

        // Calculate the camera's view dimensions based on its size and aspect ratio
        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Adjust boundaries to encompass both the current and new map areas
        topLeftX = Mathf.Min(topLeftX, newMap.transform.position.x + aspectRatio);
        topLeftY = Mathf.Max(topLeftY, newMap.transform.position.y - cameraSize);
        bottomRightX = Mathf.Max(bottomRightX, newMap.transform.position.x + newConfig.m_Width - aspectRatio);
        bottomRightY = Mathf.Min(bottomRightY, newMap.transform.position.y - newConfig.m_Height + cameraSize);
    }

}
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform target; // The playerTransform's transform
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
            SetBound(map);
        }
    }

    void Awake()
    {
        // Find the playerTransform object by tag and set the target
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void LateUpdate()
    {
        // Move the camera to follow the playerTransform within the map boundaries
        if (target != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(target.position.x, topLeftX, bottomRightX),
                Mathf.Clamp(target.position.y, bottomRightY, topLeftY),
                transform.position.z
            );
        }
    }

    public void SetBound(GameObject map)
    {
        // Set the camera boundaries based on the map size
        var config = map.GetComponent<SuperTiled2Unity.SuperMap>();
        if (config == null)
        {
            return;
        }

        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Calculate the map boundaries
        topLeftX = map.transform.position.x + aspectRatio;
        topLeftY = map.transform.position.y - cameraSize;
        bottomRightX = map.transform.position.x + config.m_Width - aspectRatio;
        bottomRightY = map.transform.position.y - config.m_Height + cameraSize;

    }

    public void ExtendBound(GameObject currentMap, GameObject newMap)
    {
        // Extend the camera boundaries to include the new map
        var currentConfig = currentMap.GetComponent<SuperTiled2Unity.SuperMap>();
        var newConfig = newMap.GetComponent<SuperTiled2Unity.SuperMap>();

        if (currentConfig == null || newConfig == null)
        {
            Debug.LogError("SuperTiled2Unity.SuperMap component missing on one of the map objects!");
            return;
        }

        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Adjust boundaries to combine the current and new map areas
        topLeftX = Mathf.Min(topLeftX, newMap.transform.position.x + aspectRatio);
        topLeftY = Mathf.Max(topLeftY, newMap.transform.position.y - cameraSize);
        bottomRightX = Mathf.Max(bottomRightX, newMap.transform.position.x + newConfig.m_Width - aspectRatio);
        bottomRightY = Mathf.Min(bottomRightY, newMap.transform.position.y - newConfig.m_Height + cameraSize);
    }
}

using UnityEngine;

public class MapTransition : MonoBehaviour
{
    public GameObject newMap;     // The map to transition to
    public GameObject currentMap; // The current map

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the transition zone
        if (collision.CompareTag("Player"))
        {
            // Extend the camera boundaries to include the new map
            var mainCamera = Camera.main.GetComponent<MainCamera>();
            mainCamera.ExtendBound(currentMap, newMap);

            // Activate the new map if it is disabled
            if (!newMap.activeSelf)
            {
                newMap.SetActive(true);
            }
        }
    }
}

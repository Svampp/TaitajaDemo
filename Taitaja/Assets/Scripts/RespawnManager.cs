﻿using UnityEngine;
/// <summary>
/// Manages the respawn points for the player and ensures the correct respawn point is used upon death.
/// </summary>
public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    // The current respawn point where the player will respawn after death
    Transform currentRespawnPoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign this instance to the static Instance variable
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate RespawnManager instances
        }
    }

    /// <summary>
    /// Sets the current respawn point to the specified Transform.
    /// This is usually called when the player reaches a new checkpoint or transitions to a new map.
    /// </summary>
    /// <param name="newRespawnPoint">The new respawn point for the player.</param>
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        currentRespawnPoint = newRespawnPoint;
    }

    /// <summary>
    /// Gets the current respawn point where the player will respawn after death.
    /// </summary>
    /// <returns>The Transform of the current respawn point.</returns>
    public Transform GetCurrentRespawnPoint()
    {
        return currentRespawnPoint;
    }

}
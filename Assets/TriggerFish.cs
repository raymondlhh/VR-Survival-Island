using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFish : MonoBehaviour
{
    public GameObject fish; // Prefab for the new weapon to be instantiated
    public Camera playerCamera; // Reference to the player's camera

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding objects are both tagged as 
        if (other.CompareTag("Fish"))
        {
            // Instantiate the combined weapon prefab in front of the player
            Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.0f;
            Instantiate(fish, spawnPosition, Quaternion.identity);
            Destroy(gameObject); // Destroy the current weapon

        }
    }
}

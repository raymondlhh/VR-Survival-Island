using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineObject : MonoBehaviour
{
    public GameObject combinedWeaponPrefab; // Prefab for the new weapon to be instantiated
    public Camera playerCamera; // Reference to the player's camera

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding objects are both tagged as "Weapon"
        if (other.CompareTag("Weapon") && gameObject.CompareTag("Weapon"))
        {
            // Instantiate the combined weapon prefab in front of the player
            Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.0f;
            Instantiate(combinedWeaponPrefab, spawnPosition, Quaternion.identity);

            // Debug log to confirm action
            Debug.Log("Both weapons have collided and a new weapon has been instantiated.");

            // Destroy both weapon objects
            Destroy(gameObject); // Destroy the current weapon
            Destroy(other.gameObject); // Destroy the colliding weapon
        }
    }
}

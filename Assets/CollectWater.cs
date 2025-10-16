using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWater : MonoBehaviour
{
    public Material originalMaterial;
    public Material newMaterial;
    private Renderer waterRenderer; // The Renderer for the water sphere

    public CookWater cookwaterScript;
     public void Start()
    {
        // Get the Renderer component and the original material
        waterRenderer = GetComponent<Renderer>();
        originalMaterial = waterRenderer.material; // Automatically get the current material

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterCanDrink") && cookwaterScript.isCooked == true) // Ensure the Fire zone is tagged correctly
        {

            if (newMaterial != null && waterRenderer != null)
            {
                waterRenderer.material = newMaterial;
                gameObject.layer = LayerMask.NameToLayer("Drink");
            }
            else
            {
                Debug.LogWarning("New material or renderer is not assigned!");
            }

            cookwaterScript.gameObject.SetActive(false);

        }

        if (other.CompareTag("Mouth"))
        {
            Debug.Log("Drink!");
            if (waterRenderer != null && originalMaterial != null)
            {
                waterRenderer.material = originalMaterial; // Revert to original material
            }
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
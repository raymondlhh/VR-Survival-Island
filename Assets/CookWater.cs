using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookWater : MonoBehaviour
{
    public Material originalMaterial;
    public Material newMaterial;
    private Renderer waterRenderer;
    private bool isChanging = false; // Prevent multiple coroutine calls
    public Image countdownText;
    public bool showUI = false;
    public bool isCooked = false;

    private void Start()
    {
        // Get the Renderer component and the original material
        waterRenderer = GetComponent<Renderer>();
        originalMaterial = waterRenderer.material; // Automatically get the current material

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire") && !isChanging)
        {
            StartCoroutine(ChangeMaterialWithDelay(10f)); // Start coroutine with 20-second delay
        }
    }

    private IEnumerator ChangeMaterialWithDelay(float delay)
    {
        isChanging = true; // Mark as changing to prevent multiple triggers
        countdownText.gameObject.SetActive(true);
        showUI = true;
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        if (newMaterial != null && waterRenderer != null)
        {
            waterRenderer.material = newMaterial;
            isCooked = true;
        }

        countdownText.gameObject.SetActive(false);
        showUI = false;
        isChanging = false; // Reset state
    }
}

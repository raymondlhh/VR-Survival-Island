using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    private Outline outline; // Reference to the Outline component

    private void Start()
    {
        // Add or retrieve the Outline component
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineColor = Color.green; // Set the desired outline color
            outline.OutlineWidth = 14.0f; // Set the desired outline width
        }

        outline.enabled = false; // Disable the outline initially
    }

    // Called when the VR hand enters the object's collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object interacting with this is tagged as "VRHand"
        if (other.CompareTag("VRHand"))
        {
            Debug.Log("Trigger Entered by VRHand");

            outline.enabled = true; // Enable the outline
        }
    }

    // Called when the VR hand exits the object's collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the object interacting with this is tagged as "VRHand"
        if (other.CompareTag("VRHand"))
        {
            outline.enabled = false; // Disable the outline
        }
    }
}

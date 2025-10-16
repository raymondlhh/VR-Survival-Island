using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObjectHandler : MonoBehaviour
{
    public EnergyBarController energyBarController; // Reference to the EnergyBarController
    public ThirstBarController thirstBarController; // Reference to the ThirstBarController

    private void OnEnable()
    {
        // Subscribe to the grab event
        var interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        var interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"OnGrab called for {gameObject.name} with tag {gameObject.tag}");

        // Handle based on the tag of the grabbed object
        if (gameObject.CompareTag("Wood") && energyBarController != null)
        {
            Debug.Log("Wood object grabbed. Decreasing energy.");
            energyBarController.DecreaseEnergy(); // Decrease energy
        }
        else if (gameObject.CompareTag("Food") && energyBarController != null)
        {
            Debug.Log("Food object grabbed. Increasing energy.");
            energyBarController.IncreaseEnergy(); // Increase energy
        }
        else if (gameObject.CompareTag("Water") && thirstBarController != null)
        {
            Debug.Log("Water object grabbed. Increasing thirst.");
            thirstBarController.IncreaseThirst(); // Increase thirst
        }
        else
        {
            Debug.Log("Unknown object grabbed: " + gameObject.tag);
        }
    }
}
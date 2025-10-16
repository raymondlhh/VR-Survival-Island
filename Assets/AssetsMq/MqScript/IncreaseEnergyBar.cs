using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IncreaseEnergyBar : MonoBehaviour
{
    public float energyIncreaseAmount = 20f; // Amount to increase energy by

    private BarController barController;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Find the BarController in the scene
        barController = FindObjectOfType<BarController>();

        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Add event listener for grab interaction
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (barController != null)
        {
            barController.IncreaseEnergy(energyIncreaseAmount);
            Debug.Log("Energy increased by: " + energyIncreaseAmount);
        }
    }

    void OnDestroy()
    {
        // Remove the event listener to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }
}

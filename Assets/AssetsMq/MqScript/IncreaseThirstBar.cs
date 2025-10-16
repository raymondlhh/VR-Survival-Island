using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IncreaseThirstBar : MonoBehaviour
{
    public float thirstIncreaseAmount = 20f; // Amount to increase thirst by

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
            barController.IncreaseThirst(thirstIncreaseAmount);
            Debug.Log("Thirst increased by: " + thirstIncreaseAmount);
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

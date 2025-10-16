using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateObjectOnGrab : MonoBehaviour
{
    public GameObject objectToActivate; // Assign this in the Inspector

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Register event handlers for grab events
        grabInteractable.onSelectEntered.AddListener(HandleGrab);
        grabInteractable.onSelectExited.AddListener(HandleRelease);
    }

    private void HandleGrab(XRBaseInteractor interactor)
    {
        // Activate the object when the item is grabbed
        if (objectToActivate != null)
            objectToActivate.SetActive(true);
    }

    private void HandleRelease(XRBaseInteractor interactor)
    {
        // Optional: Deactivate the object when the item is released
        // if (objectToActivate != null)
        //     objectToActivate.SetActive(false);
    }

    void OnDestroy()
    {
        // Make sure to unregister the event handlers when the script is destroyed
        grabInteractable.onSelectEntered.RemoveListener(HandleGrab);
        grabInteractable.onSelectExited.RemoveListener(HandleRelease);
    }
}

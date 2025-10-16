using UnityEngine;
using System.Collections;

public class ActivateOnSocket : MonoBehaviour
{
    public GameObject objectToActivate; // Drag the GameObject you want to activate in the inspector
    public float waitTime = 0f; // Time in seconds to wait before activating or deactivating

    // Call this method to start the activation process with a delay
    public void ActivateObjectWithDelay()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    // Coroutine to activate the object after a specified delay
    IEnumerator ActivateAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);

        // Activate the object
        if (objectToActivate != null)
            objectToActivate.SetActive(true);
    }

    // Call this method to start the deactivation process with a delay
    public void DeactivateObjectWithDelay()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    // Coroutine to deactivate the object after a specified delay
    IEnumerator DeactivateAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);

        // Deactivate the object
        if (objectToActivate != null)
            objectToActivate.SetActive(false);
    }
}

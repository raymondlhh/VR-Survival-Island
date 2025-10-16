using UnityEngine;
using System.Collections;

public class FireWoodDone : MonoBehaviour
{
    public GameObject objectToDeactivate; // Assign this in the Unity Inspector
    public GameObject object2ToDeactivate; // Assign this in the Unity Inspector

    public bool FireWood = false;

    void OnEnable()
    {
        // Called when the GameObject becomes active
        StartCoroutine(DeactivateAfterDelay());
    }

    IEnumerator DeactivateAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(0);

        // Check if objectToDeactivate is still not null after waiting
        if (objectToDeactivate != null)
            objectToDeactivate.SetActive(false);

        if (object2ToDeactivate != null)
            object2ToDeactivate.SetActive(false);

        FireWood = true;

    }
}

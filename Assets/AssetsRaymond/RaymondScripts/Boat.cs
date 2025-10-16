using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour
{
    public GameObject objectToDeactivate; // Assign this in the Unity Inspector
    public GameObject object2ToDeactivate; // Assign this in the Unity Inspector
    public GameObject object3ToDeactivate; // Assign this in the Unity Inspector
    public GameObject object4ToDeactivate; // Assign this in the Unity Inspector

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

        if (object3ToDeactivate != null)
            object3ToDeactivate.SetActive(false);

        if (object4ToDeactivate != null)
            object4ToDeactivate.SetActive(false);

    }

    void OnDisable()
    {
        // Called when the GameObject becomes inactive
        // Optional: Reactivate the other GameObject if needed when this one is deactivated
        // Uncomment the below code if you need the other GameObject to reactivate
        // if (objectToDeactivate != null)
        //     objectToDeactivate.SetActive(true);
    }
}

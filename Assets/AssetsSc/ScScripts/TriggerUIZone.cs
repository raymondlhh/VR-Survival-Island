using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUIZone : MonoBehaviour
{
    public UImanager manager;
    //public DestroyingBoat destroyingBoatScript; // Reference to the DestroyingBoat script
    public int uitoActive = 0;
    public GameObject ObjectToActivate; // Reference to the BoatSnap GameObject

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VRHand"))
        {
            manager.ActivateUI(uitoActive);
            ObjectToActivate.SetActive(true); // Activate the BoatSnap GameObject
            //destroyingBoatScript.StartFillTransition(true); // Start the slider transition
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VRHand"))
        {
            manager.DeactivateAllUI(uitoActive);
            ObjectToActivate.SetActive(false); // Deactivate the BoatSnap GameObject
            //destroyingBoatScript.StartFillTransition(false); // Reset the slider
        }
    }
}

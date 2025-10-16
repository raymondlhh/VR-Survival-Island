using UnityEngine;
using UnityEngine.Video;

public class TriggerCutscene : MonoBehaviour
{
    public VideoController videoController; // Reference to the VideoController

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VRHand"))
        {
            videoController.PlayVideo(1); // Play the second video
        }
    }
}
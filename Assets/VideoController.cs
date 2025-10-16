using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public Camera firstCamera;
    public Camera secondCamera;
    public AudioManager audioManager;
    public VideoClip[] videoClips;  // Array to store multiple video clips

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        PlayVideo(0);  // Automatically start the first video
        ActivateFirstCamera();  // Start with second camera for initial gameplay
    }

    public void PlayVideo(int index)
    {
        if (index >= 0 && index < videoClips.Length)
        {
            videoPlayer.clip = videoClips[index];
            canvas.SetActive(true);
            videoPlayer.Play();
            ActivateFirstCamera();  // Always activate first camera when playing any video
        }
    }

    public void ActivateFirstCamera()
    {
        if (firstCamera != null && secondCamera != null)
        {
            firstCamera.gameObject.SetActive(true);
            secondCamera.gameObject.SetActive(false);
        }
    }

    public void ActivateSecondCamera()
    {
        if (firstCamera != null && secondCamera != null)
        {
            firstCamera.gameObject.SetActive(false);
            secondCamera.gameObject.SetActive(true);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        
            Debug.Log("Video Finished!");
            canvas.SetActive(false); // Hide the canvas or transition to gameplay
            ActivateSecondCamera(); // Always reactivate second camera after any video ends

            if (audioManager != null)
            {
                audioManager.PlayBGM(); // Call the PlayBGM method from AudioManager
            }

        if (videoPlayer.clip == videoClips[2])  // Check if the third index video has just finished
        {
            Debug.Log("Third index video finished. Quitting the game.");
            Application.Quit();
        }
    }
}

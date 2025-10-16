using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenuController : MonoBehaviour
{
    [Header("Pause Menu Settings")]
    public GameObject pauseMenu;               // Pause Menu Canvas
    public GameObject leftHandRaycastObject;   // Raycast for hand controller
    public InputActionProperty pauseButton;    // Pause button input
    public InputActionProperty grabButton;     // Grab button input

    [Header("UI Buttons")]
    public Button playButton;                  // Play button
    public Button resumeButton;                // Resume button
    public Button exitButton;                  // Exit button

    private bool isPaused = false;
    private bool isHoveringPlay = false;
    private bool isHoveringResume = false;
    private bool isHoveringExit = false;

    void Start()
    {
        Debug.Log("PauseMenuController initialized");

        // Enable input actions
        pauseButton.action?.Enable();
        grabButton.action?.Enable();

        // Initialize pause menu and raycast
        pauseMenu.SetActive(true);
        leftHandRaycastObject.SetActive(true);
        Time.timeScale =0;

        // Set button visibility
        playButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);

        // Add button listeners
        playButton.onClick.AddListener(PlayGame);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(QuitGame);

        Debug.Log("Pause Menu initialized and ready.");
    }

    void Update()
    {
        // Toggle Pause Menu
        if (pauseButton.action?.WasPressedThisFrame() == true)
        {
            TogglePauseMenu();
        }

        // Handle grab button interactions
        if (grabButton.action?.WasPressedThisFrame() == true)
        {
            if (isHoveringPlay)
            {
                PlayGame();
            }
            else if (isHoveringResume)
            {
                ResumeGame();
            }
            else if (isHoveringExit)
            {
                QuitGame();
            }
        }

        // Debug Raycast visualization
        if (isPaused)
        {
            Debug.DrawRay(leftHandRaycastObject.transform.position, leftHandRaycastObject.transform.forward * 10f, Color.red);
        }
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        leftHandRaycastObject.SetActive(isPaused);

        // Reset Raycast if active
        if (isPaused)
        {
            var rayInteractor = leftHandRaycastObject.GetComponent<XRRayInteractor>();
            if (rayInteractor)
            {
                rayInteractor.enabled = false;
                rayInteractor.enabled = true;
            }
        }

        // Pause or resume game
        Time.timeScale = isPaused ? 0 : 1;

        Debug.Log(isPaused ? "Pause Menu Activated" : "Pause Menu Deactivated");
    }

    public void PlayGame()
    {
        Debug.Log("Play button clicked. Starting the game.");

        // Resume game state
        isPaused = false;
        pauseMenu.SetActive(false);
        leftHandRaycastObject.SetActive(false);

        playButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);

        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume button clicked. Resuming game.");

        isPaused = false;
        pauseMenu.SetActive(false);
        leftHandRaycastObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Exit button clicked. Quitting game.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Hover events for Play button
    public void OnHoverEnterPlay()
    {
        isHoveringPlay = true;
        Debug.Log("Hovering over Play button.");
    }

    public void OnHoverExitPlay()
    {
        isHoveringPlay = false;
        Debug.Log("Stopped hovering over Play button.");
    }

    // Hover events for Resume button
    public void OnHoverEnterResume()
    {
        isHoveringResume = true;
        Debug.Log("Hovering over Resume button.");
    }

    public void OnHoverExitResume()
    {
        isHoveringResume = false;
        Debug.Log("Stopped hovering over Resume button.");
    }

    // Hover events for Exit button
    public void OnHoverEnterExit()
    {
        isHoveringExit = true;
        Debug.Log("Hovering over Exit button.");
    }

    public void OnHoverExitExit()
    {
        isHoveringExit = false;
        Debug.Log("Stopped hovering over Exit button.");
    }

    void OnDestroy()
    {
        pauseButton.action?.Disable();
        grabButton.action?.Disable();
    }
}
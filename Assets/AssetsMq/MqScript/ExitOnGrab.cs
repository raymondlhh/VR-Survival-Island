using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExitGameOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();


        grabInteractable.onSelectEntered.AddListener(HandleGrab);
    }

    private void HandleGrab(XRBaseInteractor interactor)
    {
        Debug.Log("Exit button grabbed. Quitting the application...");
        Application.Quit();


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnDestroy()
    {

        grabInteractable.onSelectEntered.RemoveListener(HandleGrab);
    }
}

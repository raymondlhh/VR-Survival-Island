using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class SceneLoaderOnGrab : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneToLoad;

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

 
        grabInteractable.onSelectEntered.AddListener(HandleGrab);
    }

    private void HandleGrab(XRBaseInteractor interactor)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnDestroy()
    {

        grabInteractable.onSelectEntered.RemoveListener(HandleGrab);
    }
}

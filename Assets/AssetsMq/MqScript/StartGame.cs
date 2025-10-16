using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneToLoad; // Name of the scene to load

    // Method to start the game
    public void StartTheGame()
    {
        Debug.Log("Start object activated. Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}

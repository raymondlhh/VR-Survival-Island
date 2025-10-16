using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the Start button is clicked
    public void StartGame()
    {
        Debug.Log("Start button clicked. Loading the game scene...");
        SceneManager.LoadScene("Post-Processing"); 
    }

    // Called when the Exit button is clicked
    public void ExitGame()
    {
        Debug.Log("Exit button clicked. Quitting the game...");
        Application.Quit();
    }
}

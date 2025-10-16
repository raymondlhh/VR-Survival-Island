using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject[] uiPanels; // Array to hold all the UI panels

    void Update()
    {
        foreach (GameObject uiPanel in uiPanels)
        {
            if (uiPanel.activeSelf) // Only update active panels
            {
                uiPanel.transform.LookAt(Camera.main.transform);
                // Reverse the facing direction if needed
                uiPanel.transform.Rotate(0, 180, 0);
            }
        }
    }

    void Start()
    {
        // Deactivate all UI panels initially
        foreach (GameObject uiPanel in uiPanels)
        {
            uiPanel.SetActive(false);
        }
    }

    // Method to activate a specific UI panel by index
    public void ActivateUI(int index)
    {
        if (index >= 0 && index < uiPanels.Length)
        {
            uiPanels[index].SetActive(true);
        }
    }

    // Method to deactivate all UI panels
    public void DeactivateAllUI(int index)
    {
        foreach (GameObject uiPanel in uiPanels)
        {
            uiPanel.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThirstBarController : MonoBehaviour
{
    public Image thirstBar; // UI element for the thirst bar
    public float maxThirst = 100f; // Maximum thirst value
    public float currentThirst = 100f; // Current thirst value
    public float thirstDecreaseAmount =0.1f; // Amount to decrease every 10 seconds
    public float thirstIncreaseAmount = 20f;
    private float lastIncreaseTime;

    void Start()
    {
        currentThirst = maxThirst; // Initialize thirst to maximum
        UpdateThirstBar(); // Update the UI
        Debug.Log($"Thirst initialized: {currentThirst}");
        StartCoroutine(DecreaseThirstContinuously()); // Start continuous thirst decrease
    }

    // Continuously decrease thirst every 10 seconds
    IEnumerator DecreaseThirstContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f); // Wait 30 seconds
            if (Time.time - lastIncreaseTime >= 10f) // Only decrease if 10 seconds have passed since the last increase
            {
                currentThirst -= thirstDecreaseAmount;
                if (currentThirst < 0)
                {
                    currentThirst = 0;
                }
                Debug.Log($"Thirst decreased. Current thirst: {currentThirst}");
                UpdateThirstBar();
            }
        }
    }


    // Method to increase thirst (e.g., when grabbing water)
    public void IncreaseThirst()
    {
        currentThirst += thirstIncreaseAmount;
        lastIncreaseTime = Time.time; // Record the time of increase
        if (currentThirst > maxThirst) // Ensure thirst doesn't exceed maximum
        {
            currentThirst = maxThirst;
        }
        Debug.Log($"Thirst increased by {thirstIncreaseAmount}. Current thirst: {currentThirst}");
        UpdateThirstBar();
    }

    // Update the thirst bar UI
    public void UpdateThirstBar()
    {
        if (thirstBar != null)
        {
            thirstBar.fillAmount = currentThirst / maxThirst; // Update fill amount
            Debug.Log($"Thirst Bar Updated: Fill Amount = {thirstBar.fillAmount}");
        }
    }
}
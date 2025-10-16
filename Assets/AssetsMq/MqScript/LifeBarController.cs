using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeBarController : MonoBehaviour
{
    public Image lifeBar; // UI element for the life bar
    public float maxLife = 100f; // Maximum life value
    public float currentLife; // Current life value
    public float lifeDecreaseAmount = 10f; // Amount to decrease every second

    public ThirstBarController thirstBarController; // Reference to ThirstBarController
    public EnergyBarController energyBarController; // Reference to EnergyBarController

    public GameObject sleepyEffect; // GameObject for Sleepy post-processing effect
    public GameObject dizzyEffect; // GameObject for Dizzy post-processing effect

    public bool isDecreasingLife = false;

    public DayNightCycle morning;

    void Start()
    {
        currentLife = maxLife;
        UpdateLifeBar();
        StartCoroutine(DelayedStart());

        

        if (currentLife <= 0 && sleepyEffect != null)
        {
            Debug.Log("Initial check: Activating Sleepy effect.");
            sleepyEffect.SetActive(true);
        }

        if (dizzyEffect != null)
        {
            dizzyEffect.SetActive(false);
        }

    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForEndOfFrame();

        if (thirstBarController.currentThirst <= 0 || energyBarController.currentEnergy <= 0)
        {
            Debug.LogError($"Initial Thirst: {thirstBarController.currentThirst}, Initial Energy: {energyBarController.currentEnergy}");
            thirstBarController.currentThirst = thirstBarController.maxThirst;
            energyBarController.currentEnergy = energyBarController.maxEnergy;
        }

        StartCoroutine(CheckBarsAndDecreaseLife());
        StartCoroutine(CheckBarsAndEffects());
    }


    IEnumerator CheckBarsAndDecreaseLife()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (thirstBarController.currentThirst <= 0 || energyBarController.currentEnergy <= 0)
            {
                if (!isDecreasingLife) // Avoid multiple decreases at the same time
                {
                    isDecreasingLife = true;
                    StartCoroutine(DecreaseLifeContinuously(10f));
                }
            }
            else if (thirstBarController.currentThirst > 0 && energyBarController.currentEnergy > 0)
            {
                isDecreasingLife = false; // Stop life decrease
            }

            yield return null; // Wait for next frame
        }
    }

    void UpdateEffects()
    {
        UpdateSleepyEffect();
        UpdateDizzyEffect();
    }

    void UpdateSleepyEffect()
    {
        if (currentLife <= 0 && sleepyEffect != null)
        {
            if (!sleepyEffect.activeSelf)
            {
                Debug.Log("Activating Sleepy effect.");
                sleepyEffect.SetActive(true);

                StartCoroutine(DeactivateSleepyEffectAfterDelay(11f)); // Start the delay coroutine
            }
        }
        else if (currentLife > 0 && sleepyEffect != null)
        {
            if (sleepyEffect.activeSelf)
            {
                Debug.Log("Deactivating Sleepy effect.");
                sleepyEffect.SetActive(false);
                ResetBarsToMax();
            }
        }
    }

    void UpdateDizzyEffect()
    {
        if (thirstBarController != null && thirstBarController.currentThirst <= 0 && dizzyEffect != null)
        {
            if (!dizzyEffect.activeSelf)
            {
                Debug.Log("Activating Dizzy effect because thirst is 0.");
                dizzyEffect.SetActive(true);

                StartCoroutine(DeactivateEffectAfterDelay(dizzyEffect, 16f)); // Dizzy effect active for 16 seconds
            }
        }
        else if (thirstBarController != null && thirstBarController.currentThirst > 0 && dizzyEffect != null)
        {
            if (dizzyEffect.activeSelf)
            {
                Debug.Log("Deactivating Dizzy effect because thirst is more than 0.");
                dizzyEffect.SetActive(false);
            }
        }
    }


    IEnumerator CheckBarsAndEffects()
    {
        while (true)
        {
            UpdateEffects();
            yield return null;
        }
    }


    public IEnumerator DecreaseLifeContinuously(float delayTime) //&
    {
        while (isDecreasingLife && currentLife > 0)
        {
            yield return new WaitForSeconds(delayTime);
            currentLife -= lifeDecreaseAmount;
            currentLife = Mathf.Max(currentLife, 0);  // Ensure life doesn't go below 0
            Debug.Log($"Life decreased. Current life: {currentLife}");
            UpdateLifeBar();

            if (currentLife <= 0)
            {
                Debug.Log("Life reached 0. Activating Sleepy effect!");
                if (sleepyEffect != null)
                {
                    sleepyEffect.SetActive(true);
                }
                isDecreasingLife = false;  // Stop decreasing life
                ResetBarsToMax();  // Optionally reset bars
                break;  // Exit the loop
            }
        }
    }

    public void DecreaseLife50()
    {
        currentLife -= 50;  // Decrease life by 50
        currentLife = Mathf.Max(currentLife, 0);  // Ensure life doesn't go below 0
        Debug.Log($"Life decreased by 50. Current life: {currentLife}");
        UpdateLifeBar();  // Update the life bar UI

        if (currentLife <= 0)
        {
            Debug.Log("Life reached 0 due to poison!");
            if (sleepyEffect != null)
            {
                sleepyEffect.SetActive(true);  // Activate sleepy effect if life reaches 0
            }
            isDecreasingLife = false;  // Ensure we stop decreasing life
            ResetBarsToMax();  // Optionally reset bars
        }
    }


    void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            lifeBar.fillAmount = currentLife / maxLife; // Update fill amount
        }
    }

    void ResetBarsToMax()
    {
       
        // Reset Life
        currentLife = maxLife;
        UpdateLifeBar();
        Debug.Log("Life reset to max.");

        morning.timeOfDay = 9.0f;

        // Reset Thirst
        if (thirstBarController != null)
        {
            thirstBarController.currentThirst = thirstBarController.maxThirst;
            thirstBarController.UpdateThirstBar();
            Debug.Log("Thirst reset to max.");
        }

        // Reset Energy
        if (energyBarController != null)
        {
            energyBarController.currentEnergy = energyBarController.maxEnergy;
            energyBarController.UpdateEnergyBar();
            Debug.Log("Energy reset to max.");
        }

        // Restart any necessary coroutines
        StartCoroutine(CheckBarsAndDecreaseLife());
        StartCoroutine(CheckBarsAndEffects());
    }

    IEnumerator DeactivateSleepyEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        if (sleepyEffect != null && sleepyEffect.activeSelf)
        {
            Debug.Log("Deactivating Sleepy effect and resetting bars after delay.");
            sleepyEffect.SetActive(false);
            ResetBarsToMax(); // Reset all bars to their maximum values
        }
    }

    IEnumerator DeactivateEffectAfterDelay(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified duration

        if (effect != null && effect.activeSelf)
        {
            Debug.Log($"Deactivating effect: {effect.name} after {delay} seconds.");
            effect.SetActive(false);
        }
    }
}
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using System.Collections;

public class BarController : MonoBehaviour
{
    [Header("Energy Bar Settings")]
    public Image energyBar;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyDecreaseAmount;

    [Header("Life Bar Settings")]
    public Image lifeBar;
    public float maxLife = 100f;
    public float currentLife;
    public float lifeDecreaseAmount;

    [Header("Thirst Bar Settings")]
    public Image thirstBar;
    public float maxThirst = 100f;
    public float currentThirst;
    public float thirstDecreaseAmount;

    [Header("Sleepy Object Settings")]
    public GameObject sleepyObject;

    [Header("Dizzy Object Settings")]
    public GameObject dizzyObject;

    [Header("Poison Object Settings")]
    public GameObject poisonObject;
    public float poisonLifeDecrease = 20f;

    private XRGrabInteractable grabInteractable;
    private Coroutine lifeDecreaseCoroutine;
    private Coroutine thirstDecreaseCoroutine;
    private bool isPaused = false;

    void Start()
    {
        currentEnergy = maxEnergy;
        currentLife = maxLife;
        currentThirst = maxThirst;

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable?.selectEntered.AddListener(OnGrab);

        UpdateEnergyBar();
        UpdateLifeBar();
        UpdateThirstBar();

        //// Start the thirst decrease coroutine
        //thirstDecreaseCoroutine = StartCoroutine(DecreaseThirstOverTime());
    }

    // Call this method to start coroutines when the game begins
    public void StartGame()
    {
        if (thirstDecreaseCoroutine == null)
        {
            thirstDecreaseCoroutine = StartCoroutine(DecreaseThirstOverTime());//no call dao
            Debug.Log("Thirst decrease coroutine started.");
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (isPaused) return; // Do nothing if paused

        Debug.Log("Grabbed Object: " + args.interactableObject.transform.gameObject.name);

        //use if prevent decrease to negative value
        currentEnergy = currentEnergy - energyDecreaseAmount;
        Debug.Log("hit");
        UpdateEnergyBar();
        CheckLifeDecrease();


        if (currentLife == 0 && sleepyObject != null)
        {
            sleepyObject.SetActive(true);
            Debug.Log("Sleepy object activated!");
        }
    }


    IEnumerator DecreaseThirstOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (isPaused) continue;

            currentThirst = currentThirst - thirstDecreaseAmount;
            Debug.Log("Thirst decreased. Current Thirst: " + currentThirst);
            UpdateThirstBar();
        }
    }

    IEnumerator DecreaseLifeOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (isPaused) continue;

            currentLife =currentLife - lifeDecreaseAmount;
            Debug.Log("Life decreased. Current Life: " + currentLife);
            UpdateLifeBar();

            if (currentLife == 0 && sleepyObject != null)
            {
                sleepyObject.SetActive(true);
                Debug.Log("Sleepy object activated!");
                break;
            }
        }
    }

    void CheckLifeDecrease()
    {
        if (currentEnergy == 0 || currentThirst == 0)
        {
            if (lifeDecreaseCoroutine == null)
            {
                lifeDecreaseCoroutine = StartCoroutine(DecreaseLifeOverTime());
                Debug.Log("Life decrease coroutine started.");
            }
        }
        else
        {
            if (lifeDecreaseCoroutine != null)
            {
                StopCoroutine(lifeDecreaseCoroutine);
                lifeDecreaseCoroutine = null;
                Debug.Log("Life decrease coroutine stopped.");
            }
        }
    }

    public void Pause(bool pause)
    {
        isPaused = pause;

        if (pause)
        {
            if (thirstDecreaseCoroutine != null)
            {
                StopCoroutine(thirstDecreaseCoroutine);
                thirstDecreaseCoroutine = null;
                Debug.Log("Thirst decrease coroutine paused.");
            }
        }
        else
        {
            if (thirstDecreaseCoroutine == null)
            {
                thirstDecreaseCoroutine = StartCoroutine(DecreaseThirstOverTime());
                Debug.Log("Thirst decrease coroutine resumed.");
            }
        }
    }



    public void IncreaseThirst(float amount)
    {
        currentThirst = Mathf.Min(currentThirst + amount, maxThirst); // Clamp to maxThirst
        Debug.Log("Thirst increased by " + amount + ". Current Thirst: " + currentThirst);
        UpdateThirstBar();

        // Restart the coroutine with a slight delay
        if (thirstDecreaseCoroutine != null)
        {
            StopCoroutine(thirstDecreaseCoroutine);
        }
        thirstDecreaseCoroutine = StartCoroutine(RestartThirstDecrease());
    }

    // Coroutine to restart the thirst decrease after a delay
    IEnumerator RestartThirstDecrease()
    {
        Debug.Log("Thirst decrease coroutine paused briefly.");
        yield return new WaitForSeconds(5f); // 5-second delay before resuming
        thirstDecreaseCoroutine = StartCoroutine(DecreaseThirstOverTime());
        Debug.Log("Thirst decrease coroutine resumed.");
    }

    public void IncreaseEnergy(float amount)
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        Debug.Log("Energy increased by " + amount + ". Current Energy: " + currentEnergy);
        UpdateEnergyBar();
    }


    void UpdateEnergyBar()
    {
        energyBar.fillAmount = currentEnergy / maxEnergy;
    }

    void UpdateLifeBar()
    {
        lifeBar.fillAmount = currentLife / maxLife;
    }

    void UpdateThirstBar()
    {
        thirstBar.fillAmount = currentThirst / 100;
        Debug.Log("Thirst Bar Updated: Fill Amount = " + thirstBar.fillAmount);

        if (currentThirst == 0 && dizzyObject != null)
        {
            dizzyObject.SetActive(true);
            Debug.Log("Dizzy object activated!");
        }
        else if (dizzyObject != null)
        {
            dizzyObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        grabInteractable?.selectEntered.RemoveListener(OnGrab);
    }
}
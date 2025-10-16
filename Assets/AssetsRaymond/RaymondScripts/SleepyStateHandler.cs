using UnityEngine;
using System.Collections;

public class SleepyStateHandler : MonoBehaviour
{
    private Animator animator;
    private Coroutine sleepyRoutine;

    void Awake()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // Start the sleepy routine when the GameObject is enabled
        sleepyRoutine = StartCoroutine(SleepyRoutine());
    }

    void OnDisable()
    {
        // Stop the sleepy routine if the GameObject is disabled
        if (sleepyRoutine != null)
        {
            StopCoroutine(sleepyRoutine);
        }
    }

    private IEnumerator SleepyRoutine()
    {
        // Set 'isSleepy' to true initially
        animator.SetBool("isSleepy", true);

        // Wait for 10 seconds
        yield return new WaitForSeconds(11);

        // Set 'isSleepy' to false after 11 seconds
        animator.SetBool("isSleepy", false);

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
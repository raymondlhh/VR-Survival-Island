using UnityEngine;
using System.Collections;

public class DizzyStateHandler : MonoBehaviour
{
    private Animator animator;
    private Coroutine dizzyRoutine;

    void Awake()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // Start the sleepy routine when the GameObject is enabled
        dizzyRoutine = StartCoroutine(DizzyRoutine());
    }

    void OnDisable()
    {
        // Stop the sleepy routine if the GameObject is disabled
        if (dizzyRoutine != null)
        {
            StopCoroutine(dizzyRoutine);
        }
    }

    private IEnumerator DizzyRoutine()
    {
        // Set 'isSleepy' to true initially
        animator.SetBool("isDizzy", true);

        // Wait for 10 seconds
        yield return new WaitForSeconds(16);

        // Set 'isSleepy' to false after 11 seconds
        animator.SetBool("isDizzy", false);

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
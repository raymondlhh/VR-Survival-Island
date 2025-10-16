using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyingBoat : MonoBehaviour
{
    public float transitionTime = 1f;
    public Image fillImage;
    public UImanager manager;
    public GameObject objectToActivate;
    public GameObject explosionToActivate;
    public GameObject UiBreakingWoods;

    AudioManager audioManager;
    Coroutine fillCoroutine;
    

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void StartFillTransition(bool isActive)
    {
        if (isActive)
        {
            if (fillCoroutine != null)
                StopCoroutine(fillCoroutine);
            fillCoroutine = StartCoroutine(ActivateFill());
        }
        else
            ResetFill();
    }

    IEnumerator ActivateFill()
    {
        float elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            fillImage.fillAmount = Mathf.Lerp(0, 1, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fillImage.fillAmount = 1; // Ensure the fill is complete

        this.gameObject.SetActive(false);
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        if (explosionToActivate != null)
        {
            explosionToActivate.SetActive(true);
            audioManager.PlaySFX(audioManager.explosion);
        }

        if (UiBreakingWoods != null)
            UiBreakingWoods.SetActive(false);
    }

    void ResetFill()
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
            fillCoroutine = null;
        }
        fillImage.fillAmount = 0;
        this.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VRHand"))
        {
            manager.ActivateUI(1);
            StartFillTransition(true); // Start the slider transition
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VRHand"))
        {
            manager.DeactivateAllUI(1);
            StartFillTransition(false); // Reset the slider
        }
    }
}

using UnityEngine;
using System.Collections;
using FronkonGames.SpiceUp.Drunk; // Ensure this namespace is correct

public class Dizzy : MonoBehaviour
{
    public Drunk drunkEffect; // Assign this in the inspector with your Drunk component
    public float duration = 10f; // Duration over which the intensity changes
    public GameObject controlObject; // Assign a GameObject to control the effect activation

    private void OnEnable()
    {
        if (drunkEffect != null && controlObject.activeInHierarchy)
        {
            StartCoroutine(OscillateIntensity(0f, 1.0f, duration));
        }
    }

    IEnumerator OscillateIntensity(float startIntensity, float endIntensity, float time)
    {
        // Start intensity to end intensity
        yield return StartCoroutine(AdjustIntensityOverTime(startIntensity, endIntensity, time));

        // End intensity back to start intensity
        yield return StartCoroutine(AdjustIntensityOverTime(endIntensity, startIntensity, time));

        // Deactivate the control object after completing the cycle
        controlObject.SetActive(false);
    }

    IEnumerator AdjustIntensityOverTime(float startIntensity, float endIntensity, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            // Interpolate the intensity value over time
            float newIntensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / time);
            drunkEffect.settings.intensity = newIntensity; // Ensure your drunkEffect class has a public reference to settings

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the intensity is set to the final value
        drunkEffect.settings.intensity = endIntensity;
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

namespace FronkonGames.SpiceUp.Stoned
{
  /// <summary> Light intensity variations. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  [RequireComponent(typeof(Light))]
  public class LightVariation : MonoBehaviour
  {
    [SerializeField, Range(0.0f, 1.0f)]
    private float variation;

    [SerializeField] public float speed;

    private Light thelight;
    private float intensity;

    private float currentIntensity;
    private float currentSpeed;

    private void Awake()
    {
      thelight = this.GetComponent<Light>();

      this.enabled = thelight != null;
    }

    private void OnEnable()
    {
      intensity = currentIntensity = thelight.intensity;
      currentSpeed = speed;
    }

    private void Update()
    {
      thelight.intensity = Mathf.Lerp(thelight.intensity, currentIntensity, currentSpeed);

      if ((thelight.intensity - currentIntensity) <= 0.1f)
      {
        currentIntensity = Mathf.Max(0.0f,
          Random.Range(intensity - (intensity * variation), intensity + (intensity * variation)));
        currentSpeed = Random.Range(speed - (speed * variation), speed + (speed * variation));
      }
    }
  }
}
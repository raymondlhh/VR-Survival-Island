using UnityEngine;
using Random = UnityEngine.Random;

namespace FronkonGames.SpiceUp.Stoned
{
  /// <summary> The player. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  public class Player : MonoBehaviour
  {
    [Header("Potions")]

    [SerializeField, Range(0.0f, 2.0f)]
    private float raiseTime; 

    [SerializeField, Range(0.0f, 2.0f)]
    private float dropTime; 

    [SerializeField, Range(0.0f, 5.0f)]
    private float drinkingTime; 

    [SerializeField, Range(0.0f, 5.0f)]
    private float clearTime; 

    [SerializeField]
    private AnimationCurve stonedCurve; 
    
    [SerializeField]
    private Transform end; 
    
    [Header("Audio")]
    
    [SerializeField, Range(0.0f, 1.0f)]
    private float volume = 0.75f;

    [SerializeField]
    private AudioClip gulp; 

    [SerializeField]
    private AudioClip gasp; 
    
    [SerializeField]
    private AudioClip corked; 
    
    private AudioSource audioSource;

    private Camera mainCamera;
    
    private readonly RaycastHit[] hit = new RaycastHit[1];

    private Potion potion;
    
    private void StartDrinking()
    {
      PlaySound(gulp, true, volume, Random.Range(0.8f, 1.1f));
      
      Demo.settings.ResetDefaultValues();
      switch (potion.Effect)
      {
        case Potion.Effects.One:
          Demo.settings.colorStrength = new Vector3(5.0f, 0.0f, 0.0f);
          Demo.settings.tint = Color.red;
          Demo.settings.colorBlend = ColorBlends.Dodge;
          break;
        case Potion.Effects.Two:
          Demo.settings.colorStrength = new Vector3(0.0f, 5.0f, 0.0f);
          Demo.settings.tint = Color.green;
          Demo.settings.noise = 5.0f;
          Demo.settings.colorBlend = ColorBlends.Divide;
          Demo.settings.remapMax = 0.3f;
          break;
        case Potion.Effects.Three:
          Demo.settings.colorStrength = new Vector3(0.0f, 0.0f, 5.0f);
          Demo.settings.tint = Color.blue;
          Demo.settings.speed = 0.5f;
          Demo.settings.displacement = 2.0f;
          Demo.settings.colorBlend = ColorBlends.Solid;
          break;
        case Potion.Effects.Four:
          Demo.settings.lines = 0.3f;
          Demo.settings.linesStrength = 1.0f;
          Demo.settings.remapMax = 0.4f;
          Demo.settings.colorBlend = ColorBlends.Difference;
          break;
        case Potion.Effects.Five:
          Demo.settings.lines = 1.0f;
          Demo.settings.linesStrength = 1.0f;
          Demo.settings.remapMax = 0.3f;
          Demo.settings.colorBlend = ColorBlends.Color;
          break;
      }
    }
    
    private void Drinking(float progress)
    {
      Demo.settings.stoned = stonedCurve.Evaluate(progress);

      if (potion.Effect == Potion.Effects.Four)
        Demo.settings.colorHUE = progress * 2.0f % 1.0f;
    }

    private void EndDrinking()
    {
      Sequencer.Start().Then(clearTime, null, progress => Demo.settings.stoned = 1.0f - progress);
    }
    
    private void PlaySound(AudioClip clip, bool loop = false, float volume = 1.0f, float pitch = 1.0f)
    {
      if (audioSource != null)
      {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.Play();
      }
    }

    private void Awake()
    {
      audioSource = this.GetComponent<AudioSource>();
      mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
      if (potion == null &&
          Input.GetMouseButton(0) == true &&
          Physics.RaycastNonAlloc(mainCamera.ScreenPointToRay(Input.mousePosition), hit, 5.0f) > 0 &&
          hit[0].collider != null &&
          hit[0].collider.gameObject.name == "Potion")
      {
        potion = hit[0].collider.gameObject.GetComponent<Potion>();

        if (potion != null)
          Sequencer.Start()
            .Then(raiseTime, () =>
              {
                potion.Cork(false);
                PlaySound(corked);
              },
              progress =>
              {
                if (Demo.settings.stoned > 0.0f)
                  Demo.settings.stoned *= 1.0f - progress;
                
                potion.gameObject.transform.position = Vector3.Lerp(potion.OriginalPosition, end.transform.position,
                  Mathf.SmoothStep(0.0f, 1.0f, progress));
                potion.gameObject.transform.rotation = Quaternion.Slerp(potion.OriginalRotation, end.transform.rotation,
                  Mathf.SmoothStep(0.0f, 1.0f, progress));
              },
              StartDrinking)
            .Then(drinkingTime, () => PlaySound(gulp, true, volume, Random.Range(0.9f, 1.1f)), progress => Drinking(progress), EndDrinking)
            .Then(dropTime, () => PlaySound(gasp, false, volume, Random.Range(0.8f, 1.0f)),
              progress =>
              {
                potion.Cork(progress > 0.75f);
                potion.gameObject.transform.position = Vector3.Lerp(end.transform.position, potion.OriginalPosition,
                  Mathf.SmoothStep(0.0f, 1.0f, progress));
                potion.gameObject.transform.rotation = Quaternion.Slerp(end.transform.rotation, potion.OriginalRotation,
                  Mathf.SmoothStep(0.0f, 1.0f, progress));
              }, () => potion = null);
      }
    }
  }
}
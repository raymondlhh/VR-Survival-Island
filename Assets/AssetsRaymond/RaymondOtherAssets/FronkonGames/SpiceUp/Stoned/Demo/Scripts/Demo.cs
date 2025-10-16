//#define VIDEO_MODE
using System;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FronkonGames.SpiceUp.Stoned
{
#if UNITY_EDITOR
  [CustomEditor(typeof(Demo))]
  public class DemoWarning : UnityEditor.Editor
  {
    private GUIStyle Style => style ??= new GUIStyle(GUI.skin.GetStyle("HelpBox")) { richText = true, fontSize = 14, alignment = TextAnchor.MiddleCenter };
    private GUIStyle style;
    public override void OnInspectorGUI()
    {
      EditorGUILayout.TextArea($"\nThis code is only for the demo\n\n<b>DO NOT USE</b> it in your projects\n\nIf you have any questions,\ncheck the <a href='{Constants.Support.Documentation}'>online help</a> or use the <a href='mailto:{Constants.Support.Email}'>support email</a>,\n<b>thanks!</b>\n", Style);
      this.DrawDefaultInspector();
    }
  }
#endif

  /// <summary> Spice Up: Stoned demo. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  [RequireComponent(typeof(AudioSource))]
  public class Demo : MonoBehaviour
  {
    [Header("Gameplay")]

    [SerializeField]
    private Player player;
    
    [Header("UI")]
    
    [SerializeField]
    private CanvasGroup fade;

    [SerializeField]
    private CanvasGroup logo;

    [SerializeField]
    private CanvasGroup start;

    [SerializeField]
    private CanvasGroup credits;
    
    [SerializeField]
    private CanvasGroup end;
    
    [Header("Audio")]

    [SerializeField, Range(0.0f, 1.0f)]
    private float volume = 0.5f;
    
    [SerializeField]
    private AudioClip music;

    [Header("Internal")]

    public static Stoned.Settings settings;

    private AudioSource audioSource;

    private void ResetDemoValues()
    {
      settings.ResetDefaultValues();
    }

    private void Awake()
    {
      if (Stoned.IsInRenderFeatures() == false)
      {
        Debug.LogWarning($"Effect '{Constants.Asset.Name}' not found. You must add it as a Render Feature.");
#if UNITY_EDITOR
        if (EditorUtility.DisplayDialog($"Effect '{Constants.Asset.Name}' not found", $"You must add '{Constants.Asset.Name}' as a Render Feature.", "Quit") == true)
          EditorApplication.isPlaying = false;
#endif
      }

      this.enabled = Stoned.IsInRenderFeatures();
    }

    private void Start()
    {
      settings = Stoned.GetSettings();
      ResetDemoValues();

      audioSource = this.gameObject.GetComponent<AudioSource>();
      if (music != null)
      {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.PlayDelayed(1.25f);
      }
      
      Random.InitState((int)DateTime.Now.Ticks);

      player.enabled = false;

      Sequencer.Start()
        .Then(2.0f, () => fade.gameObject.SetActive(true), progress => fade.alpha = 1.0f - progress,
          () => fade.gameObject.SetActive(false))
        .Then(1.0f, () => logo.gameObject.SetActive(true), progress => logo.alpha = progress)
        .Wait(2.0f)
        .Then(1.0f, null, progress => logo.alpha = 1.0f - progress, () => logo.gameObject.SetActive(false))
        .Wait(1.0f)
#if UNITY_EDITOR && VIDEO_MODE
        .Then(() => player.enabled = true);
#else
        .Then(1.0f,
          () =>
          {
            credits.gameObject.SetActive(true);
            start.gameObject.SetActive(true);
            start.alpha = credits.alpha = 0.0f;
          },
          progress => credits.alpha = start.alpha = progress)
        .Wait(4.0f)
        .Then(1.0f, null,
          progress => credits.alpha = start.alpha = 1.0f - progress,
          () =>
          {
            credits.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
            player.enabled = true;
          });
#endif
    }

#if UNITY_EDITOR && VIDEO_MODE
    private void Update()
    {
      if (Input.GetKey(KeyCode.Escape) == true)
      {
        Sequencer.Start()
          .Then(2.0f, () =>
          {
            fade.gameObject.SetActive(true);
            fade.gameObject.GetComponentInChildren<RawImage>().color = new Color(0.45f, 0.55f, 0.61f);
          }, progress => fade.alpha = progress)
          .Then(1.0f, () => end.gameObject.SetActive(true), progress => end.alpha = progress)
          .Wait(5.0f)
          .Then(1.0f, null, progress =>
          {
            end.alpha = 1.0f - progress;
            audioSource.volume = volume * (1.0f - progress);
          })
          .Wait(0.5f)
          .Then(() => UnityEditor.EditorApplication.isPlaying = false);
      }
    }
#endif

    private void OnDestroy()
    {
      settings?.ResetDefaultValues();
    }
  }  
}

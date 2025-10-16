using System;
using UnityEngine;
using UnityEngine.Rendering;
using FronkonGames.SpiceUp.Drunk;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DrunkDemo))]
public class DrunkWarning : Editor
{
  private GUIStyle Style => style ??= new GUIStyle(GUI.skin.GetStyle("HelpBox")) { richText = true, fontSize = 14, alignment = TextAnchor.MiddleCenter };
  private GUIStyle style;
  public override void OnInspectorGUI() =>
    EditorGUILayout.TextArea($"\nThis code is only for the demo\n\n<b>DO NOT USE</b> it in your projects\n\nIf you have any questions,\ncheck the <a href='{Constants.Support.Documentation}'>online help</a> or use the <a href='mailto:{Constants.Support.Email}'>support email</a>,\n<b>thanks!</b>\n", Style);
}
#endif

/// <summary> Spice Up: Drunk demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class DrunkDemo : MonoBehaviour
{
  private Drunk.Settings settings;

  private GUIStyle styleTitle;
  private GUIStyle styleLabel;
  private GUIStyle styleButton;

  private void Awake()
  {
    if (Drunk.IsInRenderFeatures() == false)
    {
      Debug.LogWarning($"Effect '{Constants.Asset.Name}' not found. You must add it as a Render Feature.");
#if UNITY_EDITOR
      if (EditorUtility.DisplayDialog($"Effect '{Constants.Asset.Name}' not found", $"You must add '{Constants.Asset.Name}' as a Render Feature.", "Quit") == true)
        EditorApplication.isPlaying = false;
#endif
    }

    this.enabled = Drunk.IsInRenderFeatures();
  }

  private void Start()
  {
    settings = Drunk.GetSettings();
    ResetDemo();
  }

  private void OnGUI()
  {
    styleTitle = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.LowerCenter,
      fontSize = 32,
      fontStyle = FontStyle.Bold
    };

    styleLabel = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.UpperLeft,
      fontSize = 24
    };

    styleButton = new GUIStyle(GUI.skin.button)
    {
      fontSize = 24
    };

    GUILayout.BeginHorizontal("box", GUILayout.Width(450.0f), GUILayout.Height(Screen.height));
    {
      const float space = 20.0f;

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();

      GUILayout.BeginVertical();
      {
        GUILayout.Space(space);

        GUILayout.Label("DRUNK DEMO", styleTitle);

        GUILayout.Space(space * 2.0f);

        settings.drunkenness = Slider("Drunkenness", settings.drunkenness);
        settings.drunkSpeed = Slider("  Speed", settings.drunkSpeed, 0.0f, 10.0f);
        settings.drunkAmplitude = Slider("  Amplitude", settings.drunkAmplitude);

        GUILayout.Space(space);

        settings.swinging = Slider("Swinging", settings.swinging);
        settings.swingingSpeed = Slider("  Speed", settings.swingingSpeed, 0.0f, 10.0f);

        GUILayout.Space(space);

        settings.distortion = Slider("Distortion", settings.distortion);
        settings.distortionSpeed = Slider("  Speed", settings.distortionSpeed, 0.0f, 10.0f);
        settings.distortionFrequency = Slider("  Frequency", settings.distortionFrequency, 0.0f, 10.0f);

        GUILayout.Space(space);

        settings.aberration = Slider("Aberration", settings.aberration, 0.0f, 10.0f);
        settings.aberrationSpeed = Slider("  Speed", settings.aberrationSpeed, 0.0f, 10.0f);

        GUILayout.Space(space);

        settings.blink = Slider("Blink", settings.blink, 0.0f, 2.0f);
        settings.blinkSpeed = Slider("  Speed", settings.blinkSpeed, 0.0f, 10.0f);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("RESET", styleButton) == true)
        {
          settings.ResetDefaultValues();
          settings.intensity = 1.0f;
        }

        GUILayout.Space(4.0f);

        if (GUILayout.Button("ONLINE DOCUMENTATION", styleButton) == true)
          Application.OpenURL(Constants.Support.Documentation);

        GUILayout.Space(4.0f);

        if (GUILayout.Button("❤️ LEAVE A REVIEW ❤️", styleButton) == true)
          Application.OpenURL(Constants.Support.Store);

        GUILayout.Space(space * 2.0f);
      }
      GUILayout.EndVertical();

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

  private void ResetDemo()
  {
    settings.ResetDefaultValues();

    settings.intensity = 1.0f;
    settings.drunkenness = 0.25f;
  }

  private void OnDestroy()
  {
    settings?.ResetDefaultValues();
  }

  private float Slider(string label, float value, float min = 0.0f, float max = 1.0f)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private int Slider(string label, int value, int min, int max)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = (int)GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private Vector3 Vector3(string label, Vector3 value, string x = "X", string y = "Y", string z = "Z", float min = 0.0f, float max = 1.0f)
  {
    GUILayout.Label(label, styleLabel);

    value.x = Slider($"   {x}", value.x, min, max);
    value.y = Slider($"   {y}", value.y, min, max);
    value.z = Slider($"   {z}", value.z, min, max);

    return value;
  }

  private T Enum<T>(string label, T value) where T : Enum
  {
    string[] names = System.Enum.GetNames(typeof(T));
    Array values = System.Enum.GetValues(typeof(T));
    int index = Array.IndexOf(values, value);

    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      if (GUILayout.Button("<", styleButton) == true)
        index = index > 0 ? index - 1 : values.Length - 1;

      GUILayout.Label(names[index], styleLabel);

      if (GUILayout.Button(">", styleButton) == true)
        index = index < values.Length - 1 ? index + 1 : 0;
    }
    GUILayout.EndHorizontal();

    return (T)(object)index;
  }
}

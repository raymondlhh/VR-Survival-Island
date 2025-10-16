////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using static FronkonGames.SpiceUp.Drunk.Inspector;

namespace FronkonGames.SpiceUp.Drunk.Editor
{
  /// <summary>
  /// Spice up Drunk inspector.
  /// </summary>
  [CustomPropertyDrawer(typeof(Drunk.Settings))]
  public class DrunkFeatureSettingsDrawer : Drawer
  {
    private Drunk.Settings settings;

    protected override void ResetValues() => settings?.ResetDefaultValues();

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<Drunk.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Drunk.
      /////////////////////////////////////////////////
      Separator();
      
      settings.drunkenness = Slider("Drunkenness", "Drunkenness level [0, 1]. Values above 0.3 can cause dizziness in the player.", settings.drunkenness, 0.0f, 1.0f, 0.0f);
      IndentLevel++;
      settings.drunkSpeed = Slider("Speed", "Speed of the oscillations [0, 10]. Default 1.", settings.drunkSpeed, 0.0f, 10.0f, 1.0f);
      settings.drunkAmplitude = Slider("Amplitude", "Amplitude of the oscillations [0, 1]. Default 0.75.", settings.drunkAmplitude, 0.0f, 1.0f, 0.75f);
      IndentLevel--;
      
      if (settings.drunkenness > 0.8f)
        EditorGUILayout.HelpBox("Values above 0.8 can cause dizziness in the player.", MessageType.Warning);
      
      settings.swinging = Slider("Swinging", "Head swing [0, 1]. Default 0.25.", settings.swinging, 0.0f, 1.0f, 0.25f);
      IndentLevel++;
      settings.swingingSpeed = Slider("Speed", "Head swing speed [0, 10]. Default 2.", settings.swingingSpeed, 0.0f, 10.0f, 2.0f);
      IndentLevel--;

      settings.distortion = Slider("Distortion", "Distortion strength [0, 1]. Default 0.2.", settings.distortion, 0.0f, 1.0f, 0.4f);
      IndentLevel++;
      settings.distortionSpeed = Slider("Speed", "Distortion speed [0, 10]. Default 2.", settings.distortionSpeed, 0.0f, 10.0f, 4.0f);
      settings.distortionFrequency = Slider("Frequency", "Distortion waves frequency [0, 10]. Default 0.5.", settings.distortionFrequency, 0.0f, 10.0f, 1.0f);
      IndentLevel--;
      
      settings.aberration = Slider("Aberration", "Chromatic aberration [0, 10]. Default 8.", settings.aberration, 0.0f, 10.0f, 8.0f);
      IndentLevel++;
      settings.aberrationSpeed = Slider("Speed", "Chromatic aberration speed [0, 10]. Default 4.", settings.aberrationSpeed, 0.0f, 10.0f, 4.0f);
      IndentLevel--;
      
      settings.blink = Slider("Blink", "Blink strength [0, 2]. Default 1.5.", settings.blink, 0.0f, 2.0f, 1.5f);
      IndentLevel++;
      settings.blinkSpeed = Slider("Speed", "Blink speed [0, 10]. Default 1.", settings.blinkSpeed, 0.0f, 10.0f, 1.0f);
      IndentLevel--;

      settings.eye = Vector2Field("Eye", "Point of view of the eye. Default center (0.5, 0.5).", settings.eye, Vector2.one * 0.5f);
      settings.eye.x = Mathf.Clamp(settings.eye.x, -0.5f, 1.5f);
      settings.eye.y = Mathf.Clamp(settings.eye.y, -0.5f, 1.5f);
      
      float remapMin = settings.remapMin;
      float remapMax = settings.remapMax;
      MinMaxSlider("Remap drunkenness", "Drunkenness remapping range [0, 1]. Default [0, 1].", ref remapMin, ref remapMax, 0.0f, 1.0f, 0.0f, 1.0f);
      settings.remapMin = remapMin;
      settings.remapMax = remapMax;
      
      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }
      
      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

        settings.affectSceneView = Toggle("Affect the Scene View?", "Does it affect the Scene View?", settings.affectSceneView);
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "Filter mode. Default Bilinear.", settings.filterMode, FilterMode.Bilinear);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          "Render pass injection. Default BeforeRenderingPostProcessing.",
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
        settings.enableProfiling = Toggle("Enable profiling", "Enable render pass profiling", settings.enableProfiling);

        IndentLevel--;
      }
    }
  }
}

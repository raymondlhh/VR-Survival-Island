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
using static FronkonGames.SpiceUp.Stoned.Inspector;

namespace FronkonGames.SpiceUp.Stoned.Editor
{
  /// <summary> Spice up Stoned inspector. </summary>
  [CustomPropertyDrawer(typeof(Stoned.Settings))]
  public class DrunkFeatureSettingsDrawer : Drawer
  {
    private Stoned.Settings settings;

    protected override void ResetValues() => settings?.ResetDefaultValues();

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<Stoned.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Stoned.
      /////////////////////////////////////////////////
      Separator();
      
      settings.stoned = Slider("Stoned", "Stoned speed [0, 1]. Default 0.", settings.stoned, 0.0f, 1.0f, 0.0f);

      settings.speed = Slider("Speed", "Effect speed [0, 5]. Default 0.5.", settings.speed, 0.0f, 5.0f, 0.5f);
      
      settings.definition = Slider("Definition", "Definition of contours [0, 5]. Default 1.", settings.definition, 0.0f, 5.0f, 1.0f);
      
      settings.displacement = Slider("Displacement", "Chromatic deformation of the background [0, 2]. Default 0.2.", settings.displacement, 0.0f, 2.0f, 0.2f);

      settings.noise = Slider("Noise", "Noise strength [0 - 5]. Default 1.", settings.noise, 0.0f, 5.0f, 1.0f);

      settings.colorHUE = Slider("Hue", "Color wheel [0, 1]. Default 0.", settings.colorHUE, 0.0f, 1.0f, 0.0f);
      
      Vector3 colorStrength = settings.colorStrength;
      colorStrength.x = Slider("Channel red", "Red channel strength [0, 5]. Default 1.", colorStrength.x, 0.0f, 5.0f, 1.0f);
      IndentLevel++;
      colorStrength.y = Slider("Green", "Green channel strength [0, 5]. Default 1.", colorStrength.y, 0.0f, 5.0f, 1.0f);
      colorStrength.z = Slider("Blue", "Blue channel strength [0, 5]. Default 1.", colorStrength.z, 0.0f, 5.0f, 1.0f);
      IndentLevel--;
      settings.colorStrength = colorStrength;

      Vector3 colorYIQ = settings.colorYIQ;
      colorYIQ.x = Slider("Luma info", "Luma information [-10, 10]. Default 1.", colorYIQ.x, -10.0f, 10.0f, 1.0f);
      IndentLevel++;
      colorYIQ.y = Slider("In-phase", "In-phase [-10, 10]. Default 1.", colorYIQ.y, -10.0f, 10.0f, 1.0f);
      colorYIQ.z = Slider("Quadrature", "Quadrature [-10, 10]. Default 1.", colorYIQ.z, -10.0f, 10.0f, 1.0f);
      IndentLevel--;
      settings.colorYIQ = colorYIQ;

      settings.lines = Slider("Lines", "Importance of lines in the effect. Default 0.", settings.lines, 0.0f, 1.0f, 0.0f);
      IndentLevel++;
      settings.linesStrength = Slider("Strength", "Lines strength [0, 1]. Default 0.1.", settings.linesStrength, 0.0f, 1.0f, 0.1f);
      IndentLevel--;
      settings.tint = ColorField("Tint", "Color tint.", settings.tint, Color.white);

      settings.colorBlend = (ColorBlends)EnumPopup("Blend", "Blend operation with the background. Default 'Lighten'.", settings.colorBlend, ColorBlends.Lighten);
      
      float remapMin = settings.remapMin;
      float remapMax = settings.remapMax;
      MinMaxSlider("Remap stoned", "Stoned remapping range [0, 1]. Default [0, 1].", ref remapMin, ref remapMax, 0.0f, 1.0f, 0.0f, 1.0f);
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

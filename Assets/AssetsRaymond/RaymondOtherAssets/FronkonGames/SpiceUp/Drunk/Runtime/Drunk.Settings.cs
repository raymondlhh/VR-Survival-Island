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
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FronkonGames.SpiceUp.Drunk
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Settings. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class Drunk
  {
    /// <summary> Settings. </summary>
    [Serializable]
    public sealed class Settings
    {
      public Settings() => ResetDefaultValues();

#region Common settings.
      /// <summary> Controls the intensity of the effect [0, 1]. Default 1. </summary>
      /// <remarks> An effect with Intensity equal to 0 will not be executed. </remarks>
      public float intensity = 1.0f;
#endregion

#region Drunk settings.
      /// <summary> Drunkenness level [0, 1]. Default 0. </summary>
      /// <remarks> Warning: values above 0.8 can cause dizziness in the player. </remarks>
      public float drunkenness = 0.0f;

      /// <summary> Minimum value of damage remapping range [0, 1]. Default 0. </summary>
      /// <remarks> Must be lower than remapMax. </remarks>
      public float remapMin = 0.0f;

      /// <summary> Maximum value of damage remapping range [0, 1]. Default 0. </summary>
      /// <remarks> Must be greater than remapMin. </remarks>
      public float remapMax = 1.0f;
      
      /// <summary> Speed of the oscillations [0, 10]. Default 1. </summary>
      public float drunkSpeed = 1.0f;

      /// <summary> Amplitude of the oscillations [0, 1]. Default 0.75. </summary>
      public float drunkAmplitude = 0.75f;

      /// <summary> Head swing [0, 1]. Default 0.25. </summary>
      public float swinging = 0.25f;

      /// <summary> Head swing speed [0, 10]. Default 2. </summary>
      public float swingingSpeed = 2.0f;

      /// <summary> Distortion strength [0, 1]. Default 0.4. </summary>
      public float distortion = 0.4f;

      /// <summary> Distortion speed [0, 10]. Default 4. </summary>
      public float distortionSpeed = 4.0f;

      /// <summary> Distortion waves frequency [0, 10]. Default 1. </summary>
      public float distortionFrequency = 1.0f;
      
      /// <summary> Chromatic aberration [0, 10]. Default 8. </summary>
      public float aberration = 8.0f;

      /// <summary> Chromatic aberration speed [0, 10]. Default 4. </summary>
      public float aberrationSpeed = 4.0f;

      /// <summary> Blink strength [0, 2]. Default 1.5. </summary>
      public float blink = 1.5f;

      /// <summary> Blink speed [0, 10]. Default 1. </summary>
      public float blinkSpeed = 1.0f;

      /// <summary> Point of view of the eye. Default center (0.5, 0.5). </summary>
      public Vector2 eye = Vector2.one * 0.5f;
#endregion

#region Color settings.
      /// <summary> Brightness [-1.0, 1.0]. Default 0. </summary>
      public float brightness = 0.0f;

      /// <summary> Contrast [0.0, 10.0]. Default 1. </summary>
      public float contrast = 1.0f;

      /// <summary>Gamma [0.1, 10.0]. Default 1. </summary>      
      public float gamma = 1.0f;

      /// <summary> The color wheel [0.0, 1.0]. Default 0. </summary>
      public float hue = 0.0f;

      /// <summary> Intensity of a colors [0.0, 2.0]. Default 1. </summary>      
      public float saturation = 1.0f;
#endregion

#region Advanced settings.
      /// <summary> Does it affect the Scene View? </summary>
      public bool affectSceneView = false;

      /// <summary> Filter mode. Default Bilinear. </summary>
      public FilterMode filterMode = FilterMode.Bilinear;
      
      /// <summary> Render pass injection. Default BeforeRenderingPostProcessing. </summary>
      public RenderPassEvent whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
      
      /// <summary> Enable render pass profiling. </summary>
      public bool enableProfiling = false;
#endregion

      /// <summary> Reset to default values. </summary>
      public void ResetDefaultValues()
      {
        intensity = 1.0f;

        drunkenness = 0.0f;
        remapMin = 0.0f;
        remapMax = 1.0f;
        drunkSpeed = 1.0f;
        drunkAmplitude = 0.75f;
        swinging = 0.25f;
        swingingSpeed = 2.0f;
        distortion = 0.4f;
        distortionSpeed = 4.0f;
        distortionFrequency = 1.0f;
        aberration = 8.0f;
        aberrationSpeed = 4.0f;
        blink = 1.0f;
        blinkSpeed = 1.5f;
        eye = Vector2.one * 0.5f;

        brightness = 0.0f;
        contrast = 1.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        affectSceneView = false;
        filterMode = FilterMode.Bilinear;
        whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
        enableProfiling = false;
      }
    }    
  }
}

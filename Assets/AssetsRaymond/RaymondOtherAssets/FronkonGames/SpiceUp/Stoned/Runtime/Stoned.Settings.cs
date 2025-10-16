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

namespace FronkonGames.SpiceUp.Stoned
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Settings. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class Stoned
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

#region Stoned settings.
      /// <summary> Stoned level [0, 1]. Default 0. </summary>
      public float stoned = 0.0f;

      /// <summary> Minimum value of damage remapping range [0, 1]. Default 0. </summary>
      /// <remarks> Must be lower than remapMax. </remarks>
      public float remapMin = 0.0f;

      /// <summary> Maximum value of damage remapping range [0, 1]. Default 0. </summary>
      /// <remarks> Must be greater than remapMin. </remarks>
      public float remapMax = 1.0f;
      
      /// <summary> Effect speed [0, 5]. Default 0.5. </summary>
      public float speed = 0.5f;

      /// <summary> Definition of contours [0, 5]. Default 1. </summary>
      public float definition = 1.0f;

      /// <summary> Chromatic deformation of the background [0, 2]. Default 0.2. </summary>
      public float displacement = 0.2f;
      
      /// <summary> Blend operation with the background. Default 'Lighten'. </summary>     
      public ColorBlends colorBlend = ColorBlends.Lighten;
      
      /// <summary> Strength of each color channel. </summary>
      public Vector3 colorStrength = Vector3.one;
      
      /// <summary>
      /// YIQ color space.
      /// X: luma information [-10, 10]. Default 1.
      /// Y: In-phase [-10, 10]. Default 1.
      /// Z: Quadrature [-10, 10]. Default 1.
      /// </summary>
      public Vector3 colorYIQ = Vector3.one;

      /// <summary> Importance of the lines in the effect [0, 1]. Default 0. </summary>
      public float lines = 0.0f;

      /// <summary> Lines strength [0, 1]. Default 0.1. </summary>
      public float linesStrength = 0.1f;
      
      /// <summary> Color tint. </summary>
      public Color tint = Color.white;

      /// <summary> Noise strength [0 - 5]. Default 1. </summary>
      public float noise = 1.0f;
      
      /// <summary> Color wheel [0, 1]. Default 0. </summary>
      public float colorHUE = 0.0f;
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

        stoned = 0.0f;
        remapMin = 0.0f;
        remapMax = 1.0f;
        speed = 0.5f;
        definition = 1.0f;
        displacement = 0.2f;
        colorBlend = ColorBlends.Lighten;
        colorStrength = Vector3.one;
        colorYIQ = Vector3.one;
        lines = 0.0f;
        linesStrength = 0.1f;
        tint = Color.white;
        noise = 1.0f;
        colorHUE = 0.0f;

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

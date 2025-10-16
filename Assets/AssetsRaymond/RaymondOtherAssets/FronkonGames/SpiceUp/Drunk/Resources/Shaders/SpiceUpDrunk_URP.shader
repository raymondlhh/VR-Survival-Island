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
Shader "Hidden/Fronkon Games/Spice Up/Drunk URP"
{
  Properties
  {
    _MainTex("Main Texture", 2D) = "white" {}
  }

  SubShader
  {
    Tags
    {
      "RenderType" = "Opaque"
      "RenderPipeline" = "UniversalPipeline"
    }
    LOD 100
    ZTest Always ZWrite Off Cull Off

    Pass
    {
      Name "Fronkon Games Spice Up Drunk"

      HLSLPROGRAM
      #include "SpiceUp.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
      
      #pragma vertex SpiceUpVert
      #pragma fragment SpiceUpFrag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL

      float _Drunkenness;
      float _DrunkSpeed;
      float _DrunkAmplitude;
      float _Swinging;
      float _SwingingSpeed;
      float _Distortion;
      float _DistortionSpeed;
      float _DistortionFrequency;
      float _Aberration;
      float _AberrationSpeed;
      float _BlinkAmount;
      float _BlinkSpeed;
      float2 _Eye;

      inline float2x2 Rotate(float angle)
      {
        return float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
      }

      half4 SpiceUpFrag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.uv).xy;

        const half4 color = SAMPLE_MAIN(uv);
        half4 pixel = color;

        float speed = _Time.y * _DrunkSpeed;

        float2 drunkUV = mul(Rotate(sin(_Time.y * _SwingingSpeed) * PI * _Swinging), uv);
        drunkUV = lerp(uv, drunkUV, _Drunkenness);

        drunkUV.x += cos(drunkUV.y * _DistortionFrequency + _Time.y * _DistortionSpeed) * 0.01 * _Distortion;
        drunkUV.y += sin(drunkUV.x * _DistortionFrequency + _Time.y * _DistortionSpeed) * 0.01 * _Distortion;
        drunkUV.x -= cos(drunkUV.y * _DistortionFrequency + _Time.y * _DistortionSpeed) * 0.01 * _Distortion;
        drunkUV.x -= cos(drunkUV.x * _DistortionFrequency + _Time.y * _DistortionSpeed) * 0.01 * _Distortion;
        drunkUV = lerp(uv, drunkUV, _Drunkenness);
        
        const float2 center = float2(sin(speed * 1.25 + 75.0 + drunkUV.y * 0.5) + cos(speed * 2.75 - 18.0 - drunkUV.x * 0.25),
                               sin(speed * 1.75 - 125.0 + drunkUV.x * 0.25) + cos(speed * 2.25 + 4.0  - drunkUV.y * 0.5)) * _DrunkAmplitude + 0.5;
        
        drunkUV = lerp(drunkUV, (drunkUV - center) + center, _Drunkenness);
        
        const float aberrationSpeed = _Time.y * _AberrationSpeed;
        float2 aberration = float2(sin(aberrationSpeed), cos(aberrationSpeed)) * _MainTex_TexelSize.xy * _Aberration;
        aberration = lerp((float2)0.0, aberration, _Drunkenness);

        pixel.r = SAMPLE_MAIN(float2(drunkUV.x + aberration.x, drunkUV.y + aberration.y)).r;
        pixel.g = SAMPLE_MAIN(float2(drunkUV.x - aberration.y, drunkUV.y + aberration.x)).g;
        pixel.b = SAMPLE_MAIN(float2(drunkUV.x - aberration.x, drunkUV.y - aberration.y)).b;

        speed = _Time.y * _BlinkSpeed;
        const float vignette = 1.0 - distance(uv, _Eye);
        pixel *= lerp(1.0, vignette, sin((speed + 80.0) * 2.0) * _BlinkAmount * _Drunkenness);

        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb);

        return lerp(color, pixel, _Intensity);
      }
      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}

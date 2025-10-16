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
Shader "Hidden/Fronkon Games/Spice Up/Stoned URP"
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
      Name "Fronkon Games Spice Up Stoned"

      HLSLPROGRAM
      #include "SpiceUp.hlsl"
      #include "ColorBlend.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
      
      #pragma vertex SpiceUpVert
      #pragma fragment SpiceUpFrag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL

      float _Stoned;
      float _Speed;
      float _Definition;
      float _Displacement;
      int _ColorBlend;
      float _LinesStrength;
      float _Lines;
      float _Noise;
      float _ColorHUE;
      float3 _ColorStrength;
      float3 _ColorYIQ;
      float3 _Tint;

      inline float2 SinCos(const float x)
      {
        return float2(sin(x), cos(x));
      }

      inline float Simplex2D(float2 p)
      {
        const float F2 =  0.3660254;
        const float G2 = -0.2113249;

        float2 s = floor(p + (p.x + p.y) * F2),
               x = p - s - (s.x + s.y) * G2;
        
        float e = step(0.0, x.x - x.y);
        const float2 i1 = float2(e, 1.0-e),
                     x1 = x - i1 - G2,
                     x2 = x - 1.0 - 2.0 * G2;
               
        float3 w, d;
        w.x = dot(x, x);
        w.y = dot(x1, x1);
        w.z = dot(x2, x2);
        w = max(0.5 - w, 0.0);
        d.x = dot(Rand2(s + 0.0), x);
        d.y = dot(Rand2(s + i1), x1);
        d.z = dot(Rand2(s + 1.0), x2);
        
        w *= w;
        w *= w;
        d *= w;
        
        return dot(d, (float3)70.0);
      }

      inline float3 Rotate3D(float3 p, float3 v, const float phi)
      {
        v = normalize(v);
        float2 t = SinCos(-phi);
        const float s = t.x, c = t.y, x = -v.x, y = -v.y, z = -v.z;
        
        const float4x4 M = float4x4(x * x * (1.0 - c) + c,     x * y * (1.0 - c) - z * s, x * z * (1.0 - c) + y * s, 0.0,
                                    y * x * (1.0 - c) + z * s, y * y * (1.0 - c) + c,     y * z * (1.0 - c) - x * s, 0.0,
                                    z * x * (1.0 - c) - y * s, z * y * (1.0 - c) + x * s, z * z * (1.0 - c) + c,     0.0,
                                    0.0,                       0.0,                       0.0,                       1.0);

        return mul(M, float4(p, 1.0)).xyz;
      }

      inline float Trippy(float2 position, const float time)
      {
        float color = 0.0;
        const float t = 2.0 * time;
        color += sin(position.x * cos(t / 10.0) * 20.0) + cos(position.x * cos(t / 15.0) * 10.0);
        color += sin(position.y * sin(t / 5.0)  * 15.0) + cos(position.x * sin(t / 25.0) * 20.0);
        color += sin(position.x * sin(t / 10.0) * 0.2)  + sin(position.y * sin(t / 35.0) * 10.0);
        color *= sin(t * 0.1) * 0.5;

        return color;
      }

      // RGB -> YIQ.
      inline float3 RGB2YIQ(const float3 c)
      {
        return mul(float3x3(0.299, 0.587, 0.114,
                            0.596,-0.274,-0.321,
                            0.211,-0.523, 0.311), c);
      }

      // YIQ -> RGB.
      inline float3 YIQ2RGB(const float3 c)
      {
        return mul(float3x3(1.0, 0.956, 0.621,
                            1.0,-0.272,-0.647,
                            1.0,-1.107, 1.705), c);
      }

      half4 SpiceUpFrag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.uv).xy;

        const half4 color = SAMPLE_MAIN(uv);
        half4 pixel = color;

        const float speed = _Speed * _Time.y;
        
        const float2 uv2 = (uv - (float2)0.5) * 2.0;

        float3 stoned = float3(1.0, 1.0, 1.0);
        stoned = Rotate3D(stoned, float3(1.0, 1.0, 0.0), speed);
        stoned = Rotate3D(stoned, float3(1.0, 1.0, 0.0), speed);
        stoned = Rotate3D(stoned, float3(1.0, 1.0, 0.0), speed);

        const float2 v0 = _Speed * 0.5 * SinCos(0.3457 * speed + 0.3423) - Simplex2D(uv2 * 0.917),
                     v1 = _Speed * 0.5 * SinCos(0.7435 * speed + 0.4565) - Simplex2D(uv2 * 0.521),
                     v2 = _Speed * 0.5 * SinCos(0.5345 * speed + 0.3434) - Simplex2D(uv2 * 0.759);
        
        stoned = float3(dot(uv2 - v0, stoned.xy),
                        dot(uv2 - v1, stoned.yz),
                        dot(uv2 - v2, stoned.zx));

        
        stoned *= float3((16.0 * Simplex2D(uv2 + v0) + 8.0 * Simplex2D((uv2 + v0) * 2.0) + 4.0 * _Noise * Simplex2D((uv2 + v0) * 4.0) + 2.0 * Simplex2D((uv2 + v0) * 8.0) + Simplex2D((v0 + uv2) * 16.0)) / 32.0,
                         (16.0 * Simplex2D(uv2 + v1) + 8.0 * Simplex2D((uv2 + v1) * 2.0) + 4.0 * _Noise * Simplex2D((uv2 + v1) * 4.0) + 2.0 * Simplex2D((uv2 + v1) * 8.0) + Simplex2D((v1 + uv2) * 16.0)) / 32.0,
                         (16.0 * Simplex2D(uv2 + v2) + 8.0 * Simplex2D((uv2 + v2) * 2.0) + 4.0 * _Noise * Simplex2D((uv2 + v2) * 4.0) + 2.0 * Simplex2D((uv2 + v2) * 8.0) + Simplex2D((v2 + uv2) * 16.0)) / 32.0);

        const float luminance = Luminance(stoned * stoned) * _Definition;
        const float dp = clamp(ddy(-luminance) * _Definition, 0.0, 1.0);
        stoned += dp;
        
        float3 yiq = YIQ2RGB(stoned);
        yiq *= (float3)1.0 - float3(Trippy(uv2 * 0.25, speed + 0.5),
                                       Trippy(uv2 * 0.70, speed + 0.2),
                                       Trippy(uv2 * 0.40, speed + 0.7));
        yiq *= _ColorYIQ;
        stoned = lerp(stoned, RGB2YIQ(yiq), _Stoned);

        stoned.r = lerp(0.0, stoned.r, _ColorStrength.r);
        stoned.g = lerp(0.0, stoned.g, _ColorStrength.g);
        stoned.b = lerp(0.0, stoned.b, _ColorStrength.b);

        stoned *= _Tint;
        
        const float margin = _ScreenParams.x * (_Stoned * 0.5);
        const float radius = 100.0 * _Stoned;
        const float2 halfDimensions = _ScreenParams.xy * 0.5;
        const float2 pt = abs(uv * _ScreenParams.xy - halfDimensions);
        const float distance = length(pt - min(pt, halfDimensions - radius - margin));
        const float rounded = smoothstep(radius, radius + margin, distance);
        stoned *= rounded;

        stoned = lerp(saturate(stoned), (stoned - saturate(stoned * 100.0 * _LinesStrength)) * (rounded * 2.0), _Lines);

        float3 hsv = RgbToHsv(stoned);
        hsv.x += _ColorHUE;
        stoned = HsvToRgb(hsv);

        float2 displacement = Luminance(stoned * 2.0) * _Displacement * _Stoned * (1.0 - _Lines);
        pixel.r = SAMPLE_MAIN(uv + displacement).r;
        pixel.g = SAMPLE_MAIN(uv + displacement.yx).g;
        pixel.b = SAMPLE_MAIN(uv - displacement).b;

        pixel.rgb = lerp(pixel.rgb, ColorBlend(_ColorBlend, pixel.rgb, stoned), _Stoned);

        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb);

        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}

Shader "Custom/Gun Overheat" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_OverheatColour("Overheat Color", Color) = (1,0,0,1)
		_OverheatMinColour("Overheat Min Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_OverheatTex("Overheat Texture", 2D) = "white"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		#include "UnityCG.cginc"

		sampler2D _OverheatTex;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_OverheatTex;
		};

		half _Glossiness;
		half _Metallic;
		half _OverheatValue;
		half _OverheatMaxValue;
		float4 _OverheatColour;
		float4 _OverheatMinColour;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		float4 colCalc(float _a) {

			float dist = _a - _OverheatValue;

			if (dist < _OverheatValue)
				return 1;

			float c = clamp((1 - (dist - _OverheatValue) / (_OverheatMaxValue - _OverheatValue)), 0, 1);
			float4 colour = _OverheatColour + _OverheatValue*(_OverheatMinColour - _OverheatColour);

			return colour;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 s = tex2D(_OverheatTex, IN.uv_OverheatTex);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			if(_OverheatValue > s.r){
				c *= _OverheatMinColour + _OverheatColour * _OverheatValue;
			}
			else
				c *= _Color;

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

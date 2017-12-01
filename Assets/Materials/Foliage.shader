Shader "Foliage"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_AlphaTex("Alpha Texture", 2D) = "white"{}
	_NoiseTexture("Sway Noise", 2D) = "white" {}
	_SwayTexture("Sway Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "IgnoreProjector" = "True" "RenderType" = "AlphaTest" }
		LOD 100

		//ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	sampler2D _AlphaTex;
	sampler2D _NoiseTexture;
	sampler2D _SwayTexture;

	float4 _MainTex_ST;

	half _SwayStrength;
	half _SwaySpeed;

	v2f vert(appdata v)
	{
		half4 wPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));

		half4 swayAmount = tex2Dlod(_SwayTexture, float4(v.vertex.x, v.vertex.y, 0, 0));
		half4 noiseAmount = tex2Dlod(_NoiseTexture, float4(wPos.xy, 0, 0));

		half amt = (sin(_Time.y*swayAmount.r*noiseAmount.r*_SwaySpeed)) * (_SwayStrength / 100) * noiseAmount.r;

		v.vertex.x += v.uv.y * amt;

		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
		
	fixed4 aCol = tex2D(_AlphaTex, i.uv);

		if (aCol.r <= 0.9f)
			discard;

		return col;
	}
		ENDCG
	}
	}
}

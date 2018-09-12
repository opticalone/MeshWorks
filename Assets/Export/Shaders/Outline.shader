﻿Shader "Custom/Outline" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("outline width", Float)= 0.5
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	struct appdata
	{
		float4 vertex : POSITION;

	};
	struct v2f
	{
		float4 pos : POSITION;

	};
	sampler2D _MainTex;
	half _Glossiness;
	half _Metallic;
	fixed4 _Color;
	fixed4 _OutlineColor;
	float _OutlineWidth;
	ENDCG
	SubShader 
	{
		Tags{"Queue" = "Transparent" "IgnoreProjector" = "True"}
		Pass
		{
			ZWrite Off
			Cull Back
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			v2f vert(appdata v)
			{
				appdata original = v;
				v.vertex.xyz += _OutlineWidth * normalize(v.vertex.xyz);
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 frag(v2f i) : Color
			{
				return _OutlineColor;
			}
			ENDCG
		}
		Tags{"Queue" = "Geometry"}
			CGPROGRAM
			#pragma surface surf Standard 
			struct Input
			{ 
				float2 uv_MainTex;
			};
			
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex)* _Color;
			o.Albedo = c.rgb;
			
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}

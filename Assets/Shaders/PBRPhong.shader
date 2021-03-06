﻿Shader "Custom/PBRPhong" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "white" {}
		_SecondaryTex("Albedo (RGB)", 2D) = "white" {}
		_Shinyness ("Shinyness", Range(0.3,1)) = 0.5
		_SpecColor("Specular Color", Color) = (1,1,1,1)		
		_AlbedoLerp("Lerp Value", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Phong fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecondaryTex;
		sampler2D _NormalMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondaryTex;
		};

		float _AlbedoLerp;
		float _Shinyness;
		fixed4 _Color;

		inline void LightingPhong_GI(SurfaceOutput s, UnityGIInput data, UnityGI gi)
		{
			gi = UnityGlobalIllumination(data, 1.0 , s.Normal);
		}

		inline fixed4 LightingPhong(SurfaceOutput s, half3 viewDir, UnityGI gi)
		{
			const float pi = 3.141592653589793238414;
			UnityLight light = gi.light;
			float nl = max(0.0, dot(s.Normal, light.dir));
			float3 diffuseTerm = nl * s.Albedo.rgb * light.color;
			float norm = (_Shinyness + 2) / (2 + pi);
			float3 reflectionDir = reflect(-light.dir, s.Normal);
			float3 specularDot = max(0.0, dot(viewDir, reflectionDir));
			float3 spec = pow(specularDot, _Shinyness)*norm;
			float3 specTerm = spec * _SpecColor.rgb * light.color.rgb;
			float3 finalColor = diffuseTerm + specTerm;

			fixed4 c;
			c.rgb = finalColor;
			c.a = s.Alpha;
#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
			c.rgb += s.Albedo + gi.indirect.diffuse;
#endif
				return c;
		}


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutput o) {

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 c2 = tex2D(_SecondaryTex, IN.uv_SecondaryTex);
			fixed4 col = lerp(c, c2, _AlbedoLerp) * _Color;
			o.Albedo = col.rgb;
			float3 norm = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
			o.Normal = norm;
			// Metallic and smoothness come from slider variables
			
			o.Specular = _Shinyness;
			o.Gloss = col.a;
			o.Alpha = 1.0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

Shader "Phong Tessellation" {
	Properties{
		_EdgeLength("Edge length", Range(2,50)) = 5
		_Phong("Phong Strengh", Range(0,1)) = 0.5
		_MainTex("Base (RGB)", 2D) = "white" {}
		_DisplacementMap("Displacement Map (R)", 2D) = "black" {}
		_Color("Color", color) = (1,1,1,0)
		_DisplacementAmmount("Displacement Ammount", Float) = 0.1
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 300

		CGPROGRAM
#pragma surface surf Lambert vertex:displacement tessellate:tessEdge tessphong:_Phong nolightmap
#include "Tessellation.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float2 texcoord : TEXCOORD0;
	};

		fixed4 _Color;
		sampler2D _MainTex;
		sampler2D _DisplacementMap;
		float _DisplacementAmmount;

	void displacement(inout appdata v) 
	{
		float disp = tex2Dlod(_DisplacementMap, float4(v.texcoord.xy,0,0)).g *_DisplacementAmmount;
		v.vertex.xyz += v.normal * _DisplacementAmmount * (1 - disp);
	}

	float _Phong;
	float _EdgeLength;

	float4 tessEdge(appdata v0, appdata v1, appdata v2)
	{
		return UnityEdgeLengthBasedTess(v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
	}

	struct Input {
		float2 uv_MainTex;
	};

	

	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}

	ENDCG
	}
		FallBack "Diffuse"
}
Shader "Unlit/BasicSpec"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color",Color) = (0,1,0,1)
		_Ambient("Ambient", Range(0,1)) = 0.25
		_SpecColor("Spec Color",Color) = (0,1,0,1)
		_Shininess("Shiny", Float) = 10
	}
	SubShader
	{
		Tags { "LightMode"="ForwardBase" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;			
				float4 vertexClip : SV_POSITION;
				float4 vertexWorld : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _Color;
			float _Ambient;
			float _Shininess;
			//float4 _SpecColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertexClip = UnityObjectToClipPos(v.vertex);
				o.vertexWorld = mul(unity_ObjectToWorld, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldNormal = worldNormal;

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 normalDir = normalize(i.worldNormal);
				float3 viewDir = normalize(UnityWorldSpaceViewDir(i.vertexWorld));
				float3 lightDir = normalize(UnityWorldSpaceLightDir(i.vertexWorld));
				//directional light
				float nl = max(_Ambient, dot(normalDir, lightDir));
				float4 diffuseTerm = nl * _Color * _LightColor0;
				//specular
				float3 reflectionDir = reflect(-lightDir, normalDir);
				float3 specDot = max(0.0, dot(viewDir, reflectionDir));
				float3 spec = pow(specDot, _Shininess);
				float4 specTerm = float4(spec, 1) * _SpecColor, _LightColor0;

				//final color
				float4 finalColor = diffuseTerm + specTerm;
				return finalColor;
			}
			ENDCG
		}
	}
}

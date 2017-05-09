Shader "Test" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_Parallax("Height", Range(0.0, 1.0)) = 0

		_MainTex("Base (RGB)", 2D) = "white" {}

		_Metallic("Metallic", 2D) = "black" {}

	_BumpMap("Normalmap", 2D) = "bump" {}
	_ParallaxMap("Heightmap (A)", 2D) = "black" {}
	_Occlusion("Occlusion Map", 2D) = "white" {}

	_EdgeLength("Edge length", Range(3,50)) = 10
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 800

		Cull Off
		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#include "Tessellation.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float4 tangent : TANGENT;
		float3 normal : NORMAL;
		float2 texcoord : TEXCOORD0;
		float2 texcoord1 : TEXCOORD1;
	};

	float _EdgeLength;
	float _Parallax;

	float4 tessEdge(appdata v0, appdata v1, appdata v2)
	{
		return UnityEdgeLengthBasedTessCull(v0.vertex, v1.vertex, v2.vertex, _EdgeLength, _Parallax * 1.5f);
	}

	sampler2D _ParallaxMap;

	void disp(inout appdata v)
	{
		float d = tex2Dlod(_ParallaxMap, float4(v.texcoord.xy,0,0)).a * _Parallax;
		v.vertex.xyz += v.normal * d;
	}

	sampler2D _MainTex;
	sampler2D _BumpMap;
	sampler2D _Metallic;
	sampler2D _Occlusion;
	fixed4 _Color;
	half _Shininess;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float2 uv_GlossMap;
		float2 uv_SpecMap;
	};

	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 Occ = tex2D(_Occlusion, IN.uv_MainTex);
		float3 m = tex2D(_Metallic, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color.rgb * Occ.a;
		o.Metallic = m;
		o.Alpha = _Color.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG
	}

		FallBack "Bumped Specular"
}

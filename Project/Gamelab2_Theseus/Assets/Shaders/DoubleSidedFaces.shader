Shader "Custom/DoubleSidedFaces" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "black" {}
		_MetallicIntensity("Metallic Intensity", Range(0,1.5)) = 1
		_Normal("Normal",2D) = "bump" {}
		_Occlusion("Occlusion",2D) = "white" {}

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Cull Off
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _Metallic;
		sampler2D _Occlusion;
		float _MetallicIntensity;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			float3 n = UnpackNormal(tex2D(_Normal, IN.uv_MainTex));
			float3 m = tex2D(_Metallic, IN.uv_MainTex);
			fixed4 occ = tex2D(_Occlusion, IN.uv_MainTex);
			// Metallic and smoothness come from slider variables

			o.Albedo = c.rgb * _Color * occ.rgb;
			o.Normal = n;
			o.Metallic = m;
			o.Smoothness = _MetallicIntensity;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

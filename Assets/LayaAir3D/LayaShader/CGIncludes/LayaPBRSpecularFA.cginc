﻿fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;

sampler2D _SpecGlossMap;
half _Glossiness;
half _GlossMapScale;
half _Cutoff;

sampler2D _BumpMap;
float _BumpScale;

sampler2D _ParallaxMap;
half _Parallax;

sampler2D _OcclusionMap;
half _OcclusionStrength;

sampler2D _EmissionMap;
fixed4 _EmissionColor;

#include "AutoLight.cginc"
#include "Lighting.cginc"
#include "libs/LayaPBRSpecularLighting.cginc"
#include "libs/LayaUtil.cginc"
#include "UnityStandardUtils.cginc"
#include "UnityCG.cginc" 

struct a2v {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float4 tangent : TANGENT;
	float4 texcoord : TEXCOORD0;
	float4 texcoord1: TEXCOORD1;
};
			
struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float2 uv2: TEXCOORD1;  
	float3 worldPos : TEXCOORD2;
	float3 lightDir: TEXCOORD3;
	float3 viewDir : TEXCOORD4;
	SHADOW_COORDS(5)
	UNITY_FOG_COORDS(6)
};

v2f vert(a2v v) {

	v2f o;

	o.pos = UnityObjectToClipPos(v.vertex);

	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

	o.uv2 = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;

	fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);  
	fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);  
	fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w; 
	float3x3 worldToTangent = float3x3(worldTangent, worldBinormal, worldNormal);

	o.lightDir = mul(worldToTangent, WorldSpaceLightDir(v.vertex));
	o.viewDir = mul(worldToTangent, WorldSpaceViewDir(v.vertex));

	// Pass shadow coordinates to pixel shader
	TRANSFER_SHADOW(o);
	UNITY_TRANSFER_FOG(o, o.pos);

	return o;
}
			
fixed4 frag(v2f i) : SV_Target {

	fixed3 lightDir = normalize(i.lightDir);
	fixed3 viewDir = normalize(i.viewDir);

	#ifdef ParallaxTexture
		half h = tex2D (_ParallaxMap, i.uv).g;
		float2 offset = LayaParallaxOffset (h, _Parallax, i.viewDir);
		i.uv += offset;
	#endif

	fixed4 diffuseColor = tex2D(_MainTex, i.uv) * _Color;

	#if EnableAlphaCutoff
		clip (diffuseColor.a - _Cutoff);
	#endif

	fixed4 lightMapColor = fixed4(0.0,0.0,0.0,1.0);
	#if defined(LIGHTMAP_ON)
		lightMapColor = fixed4(DecodeLightmap (UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2)), 1.0); 
	#endif

	fixed4 occlusionColor = tex2D(_OcclusionMap, i.uv) * LayaOcclusion(i.uv);

	fixed3 GI = (occlusionColor * UNITY_LIGHTMODEL_AMBIENT + lightMapColor).rgb; 

	fixed3 normal = LayaUnpackScaleNormal(tex2D(_BumpMap, i.uv), _BumpScale);

	fixed4 specularGloss = SpecularGloss(i.uv);

	fixed4 color = LayaPBRSpecularLighting(diffuseColor, specularGloss.xyz, specularGloss.w, normal, viewDir, lightDir, GI);
	
	#ifdef EnableEmission
		color += tex2D(_EmissionMap, i.uv) * _EmissionColor;
	#endif

	UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

	color = fixed4(color.rgb * atten, diffuseColor.a);

	UNITY_APPLY_FOG(i.fogCoord, color);

	return color;
}

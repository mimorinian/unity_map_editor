#include "LayaBRDF.cginc"

half2 MetallicGloss(float2 uv)
{
    half2 mg;

	#ifdef MetallicGlossTexture
		#ifdef Smoothness_DiffuseTexture_Alpha
			mg.r = tex2D(_MetallicGlossMap, uv).r;
			mg.g = tex2D(_MainTex, uv).a;
		#else
			mg = tex2D(_MetallicGlossMap, uv).ra;
		#endif
		mg.g *= _GlossMapScale;
	#else
		mg.r = _Metallic;
		#ifdef Smoothness_DiffuseTexture_Alpha
			mg.g = tex2D(_MainTex, uv).a * _GlossMapScale;
		#else
			mg.g = _Glossiness;
		#endif
	#endif

    return mg;
}

inline half4 LayaPBRStandardLighting (half3 diffuseColor, half metallic, half smoothness, half3 normal, half3 viewDir, half3 lightDir, half3 GI)
{
    normal = normalize(normal);

    half oneMinusReflectivity;
    half3 specColor;
    diffuseColor = DiffuseAndSpecularFromMetallic (diffuseColor, metallic, /*out*/ specColor, /*out*/ oneMinusReflectivity);

    half4 c = BRDF1_Laya_PBS (diffuseColor, specColor, oneMinusReflectivity, smoothness, normal, viewDir, lightDir, GI);
    c.a = 1.0;
    return c;
}
